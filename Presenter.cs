using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace PaintDotCat
{
	/// <summary>
	/// View と Contnt の間．
	/// * このAPPのロジックそのもの
	/// * Content の保持者
	/// * Viewに対して表示更新を指示
	/// </summary>
	public class Presenter
		: IToolViewOpListener
		, IColorViewOpListener
	{
		//画像データ
		private Content m_Content;
		//Viewへの参照
		private readonly IView m_IView;

		//ステート
		private readonly NormalState m_NormalState;
		private readonly NewAreaSelectedState m_NewAreaSelectedState;
		private readonly ImgFloatingState m_ImgFloatingState;
		private IState m_CurrState;

		//表示の拡大率
		private int m_ViewMagRate = 1;
		//現在選択されているツールの種類
		private ToolType m_CurrSelToolType = ToolType.Pen;

		//描画色｛左，右｝
		private Color[] m_DrawColor;
		
		//編集履歴
		private const int SNAP_SHOT_STEPS = 10;	//画像全体のスナップショットを履歴とする頻度
		private List< IEdit > m_EditHistory = new List<IEdit>();
		private int m_iHistoryPointer = 0;

		//グリッド表示
		private bool m_GridVisible = false;
		private Size m_GridSize = new Size(1,1);
		private Color m_GridColor = Color.Gray;

		//-----------------------------------
		
		/// <summary>ctor</summary>
		/// <param name="w">初期の画像サイズ</param>
		/// <param name="h">初期の画像サイズ</param>
		/// <param name="View">ViewをDI</param>
		public Presenter( int w, int h, IView View )
		{
			m_NormalState = new NormalState(this);
			m_NewAreaSelectedState = new NewAreaSelectedState(this);
			m_ImgFloatingState = new ImgFloatingState(this);
			ChangeCurrStateTo( m_NormalState );

			m_DrawColor = new Color[2]{ Color.Black, Color.White };

			{
				//※実装MEMO :
				//起動時にまだ View がまともに生成されていない段階で
				//StartNew() の中から View への更新処理を呼んでしまわないように
				//ここでの m_IView への引数値の格納は StartNew() の実施よりも後で行う．
				StartNew( w,h );
				m_IView = View;
			}
		}

		//-----------------------------------
		#region 画像サイズ/Save/Load 系 public

		/// <summary>現在の画像サイズ</summary>
		public int ImgWidth{	get{	return m_Content.Width;	}	}
		/// <summary>現在の画像サイズ</summary>
		public int ImgHeight{	get{	return m_Content.Height;	}	}

		/// <summary>
		/// 画像と，編集履歴をリセットする．
		/// （「新規作成」に対応した処理）
		/// </summary>
		/// <param name="w">画像サイズ</param>
		/// <param name="h">画像サイズ</param>
		public void StartNew( int w, int h )
		{
			if( m_CurrState.IsBusy() )return;
			m_Content = new Content( w,h );
			DiscardEditHistoryData();
			LastSaveLoadFilePath = "";
			DiscardCurrSelection();
			IsImageJustAfterInitialized = true;

			m_IView?.OnImgSizeChanged();
		}

		/// <summary>画像サイズの変更</summary>
		/// <param name="w">新しいサイズ</param>
		/// <param name="h">新しいサイズ</param>
		public void ChangeImgSizeTo( int w, int h )
		{
			if( m_CurrState.IsBusy() )return;
			var Editor = new ChangeImgSize( new Size(w,h), m_DrawColor[1] );
			EditWith( Editor );
			DiscardCurrSelection();
		}

		/// <summary>
		/// 指定のファイル名で保存する．
		/// 保存処理自体の失敗時には例外が送出されるので呼び出し側でcatchする必要がある．
		/// </summary>
		/// <param name="SaveFilePath">保存パス</param>
		/// <returns>
		/// 実施したか否か．状況的に実施できない場合にはfalseを返す．
		/// 実施した（成功した）場合にはtrue．
		/// </returns>
		public bool SaveAs( string SaveFilePath )
		{
			if( m_CurrState.IsBusy() )return false;

			var FileFormat = FileFormatFromPathName( SaveFilePath );
			if( FileFormat==null )
			{	throw new ArgumentException( "Invalid File Extension" );	}

			m_Content.SaveAs( SaveFilePath, FileFormat );
			LastSaveLoadFilePath = SaveFilePath;
			return true;
		}

		/// <summary>
		/// 現在の画像をモノクロBMPに変換したものを出力する．
		/// この処理は <see cref="LastSaveLoadFilePath"/> を変更しない．
		/// </summary>
		/// <param name="SaveFilePath">保存パス</param>
		/// <returns>
		/// 実施したか否か．状況的に実施できない場合にはfalseを返す．
		/// 実施した（成功した）場合にはtrue．
		/// </returns>
		public bool ExportAsMonoBMP( string SaveFilePath )
		{
			if( m_CurrState.IsBusy() )return false;
			m_Content.ExportAsMonoBMP( SaveFilePath );
			return true;
		}

		/// <summary>
		/// 選択範囲を指定ファイル名で保存する．
		/// 保存処理自体の失敗時には例外が送出されるので呼び出し側でcatchする必要がある．
		/// </summary>
		/// <param name="SaveFilePath">保存パス</param>
		/// <returns>
		/// 実施したか否か．状況的に実施できない場合にはfalseを返す．
		/// 実施した（成功した）場合にはtrue．
		/// </returns>
		public bool SaveSelectedAreaAs( string SaveFilePath )
		{
			if( m_CurrState.IsBusy() )return false;

			var FileFormat = FileFormatFromPathName( SaveFilePath );
			if( FileFormat==null )
			{	throw new ArgumentException( "Invalid File Extension" );	}

			var AreaImg = m_CurrState.CreateSelectedAreaImg();
			if( AreaImg == null )return false;

			AreaImg.Save( SaveFilePath, FileFormat );
			return true;
		}

		/// <summary>
		/// ファイルを読み込む．
		/// 読込処理自体の失敗時には例外が送出されるので呼び出し側でcatchする必要がある．
		/// なお，その際には現在の画像データは保持される．
		/// </summary>
		/// <param name="LoadFilePath">読込パス</param>
		/// <returns>
		/// 実施したか否か．状況的に実施できない場合にはfalseを返す．
		/// 実施した（成功した）場合にはtrue．
		/// </returns>
		public bool LoadFrom( string LoadFilePath )
		{
			if( m_CurrState.IsBusy() )return false;

			var FileFormat = FileFormatFromPathName( LoadFilePath );
			if( FileFormat==null )
			{	throw new ArgumentException( "Invalid File Extension" );	}

			//※実装MEMO :
			//Bitmapのctor や Image.FromFile() で読み込んだ場合，
			//その結果をDispose()するまでファイルがロックされるとかいう話がある
			Bitmap NewBMP32 = null;
			using( var Loaded = Image.FromFile( LoadFilePath ) )
			{
				NewBMP32 = new Bitmap( Loaded );
			}

			//
			m_Content.ChangeTo( NewBMP32 );
			DiscardEditHistoryData();
			DiscardCurrSelection();
			LastSaveLoadFilePath = LoadFilePath;
			IsImageJustAfterInitialized = true;
			m_IView?.OnImgSizeChanged();
			return true;
		}

		/// <summary>
		/// 最後に Save/Load に成功したファイルパス．
		/// 一度も実施していない場合は空．
		/// <see cref="StartNew"/>を実施した場合にも空に戻る．
		/// </summary>
		public string LastSaveLoadFilePath{	get;	private set;	}

		/// <summary>
		/// 反転/回転 の操作に対応した処理の実施
		/// </summary>
		/// <param name="ActType">操作種類</param>
		public void RotateFlip( RotateFlipType ActType )
		{
			if( m_CurrState.IsBusy() )return;
			m_CurrState.RotateFlip( ActType );
		}

		#endregion
		//-----------------------------------
		#region 表示系 public

		/// <summary>View側がモーダルダイアログを表示してもよい状況か？（View側からの状況問合用）</summary>
		/// <returns>可否．他所にフォーカスが行くと困るような状況ではfalse</returns>
		public bool CanShowModalDlg(){	return !m_CurrState.IsBusy();	}

		/// <summary>
		/// 表示系の初期処理用
		///		<see cref="IVew"/>の各メソッドを呼ぶ
		/// </summary>
		public void ViewInitialization()
		{
			m_IView.OnToolSelectionChangedTo( m_CurrSelToolType );
			m_IView.OnImgSizeChanged();
			m_IView.OnViewMagRateChanged();
			m_IView.OnSelectionStateChanged( false );
			for( int i=0; i<2; ++i )m_IView.OnSelectedColorChanged( i, m_DrawColor[i] );
		}

		/// <summary>表示拡大率．整数倍のみ．</summary>
		public int ViewMagnificationRate
		{
			get{	return m_ViewMagRate;	}
			set
			{
				int NewVal = Math.Max( value, 1 );
				if( NewVal != m_ViewMagRate )
				{
					m_ViewMagRate = NewVal;
					m_IView.OnViewMagRateChanged();
				}
			}
		}

		/// <summary>画像表示域の内容を描画する</summary>
		/// <param name="g">画像表示域に描画するための Graphics</param>
		public void DrawCurrentView( Graphics g )
		{	m_CurrState.DrawCurrentView(g);	}

		/// <summary>サムネイル表示内容を描画する</summary>
		/// <param name="g">サムネイル表示域に描画するための Graphics</param>
		public void DrawThumbnail( Graphics g ){	m_CurrState.DrawThumbnail(g);	}

		/// <summary>グリッドのサイズ</summary>
		public Size GridSize
		{	
			get{	return m_GridSize;	}
			set
			{
				if( value.Width<1 || value.Height<1 )return;
				if( m_GridSize.Equals( value ) )return;
				m_GridSize = value;
				m_IView?.OnImgPainted();
			}
		}

		/// <summary>グリッドの描画色</summary>
		public Color GridColor
		{
			get{	return m_GridColor;	}
			set
			{
				if( m_GridColor.Equals( value ) )return;
				m_GridColor = value;
				m_IView?.OnImgPainted();
			}
		}
		/// <summary>
		/// グリッドを表示するか否か．
		/// ただし，trueの状態でも表示倍率が一定以上の場合にしかグリッドは表示されない．
		/// （→ <see cref="MinViewMagnificationRate_for_GridToBeShown"/>）
		/// </summary>
		public bool GridVisible
		{
			get{	return m_GridVisible;	}
			set
			{
				if( m_GridVisible == value )return;
				m_GridVisible = value;
				m_IView?.OnImgPainted();
			}
		}

		/// <summary>
		/// グリッドが描画される最小の表示倍率．
		/// （表示倍率がこの値未満である場合，<see cref="GridVisible"/>がtrueであっても描画されない）
		/// </summary>
		public int MinViewMagnificationRate_for_GridToBeShown => 4;

		#endregion
		//-----------------------------------
		#region マウス,キー操作時 public

		/// <summary>キー入力に対する処理の実施</summary>
		/// <param name="keys">入力キー情報</param>
		/// <returns>
		/// 処理を実施した場合にはtrueを返す．
		/// trueが返された場合には，GUIにキー入力に対応した動作を行わせてはならない．
		/// </returns>
		public bool ProcessKeyInput( System.Windows.Forms.Keys keys )
		{	return m_CurrState.ProcessKeyInput( keys );	}

		/// <summary>画像表示領域内でのマウスボタン押下時の処理の実施</summary>
		/// <param name="ViewPos">表示画像上での座標</param>
		/// <param name="button">押されたボタン</param>
		public void OnMouseDown( Point ViewPos, System.Windows.Forms.MouseButtons button )
		{	m_CurrState.OnMouseDown( CvtToImgPoint( ViewPos ), button );	}

		/// <summary>画像表示領域内でのマウスボタン解放時の処理の実施</summary>
		/// <param name="ViewPos">表示画像上での座標</param>
		/// <param name="button">押されたボタン</param>
		public void OnMouseUp( Point ViewPos, System.Windows.Forms.MouseButtons button )
		{	m_CurrState.OnMouseUp( CvtToImgPoint( ViewPos ), button );	}

		/// <summary>画像表示領域内（マウスキャプチャ中ならば座標は外側になり得るが）でのマウス移動時の処理の実施</summary>
		/// <param name="ViewPos">表示画像上での座標</param>
		public void OnMouseMove( Point ViewPos )
		{
			var ImgPos = CvtToImgPoint( ViewPos );
			m_IView.UpdateDisplayOfCursorPos( ImgPos );
			m_CurrState.OnMouseMove( ImgPos );
		}

		/// <summary>画像表示域ではない箇所（外側の何もない箇所）がクリックされた際の処理の実施</summary>
		/// <param name="button">ボタン</param>
		public void OnOutsidePosClicked( System.Windows.Forms.MouseButtons button )
		{	m_CurrState.OnOutsidePosClicked( button );	}

		#endregion
		//-----------------------------------
		#region 選択/Copy/Paste系 public

		/// <summary>「全選択」操作に対応する処理</summary>
		public void SelectAll()
		{
			if( m_CurrState.IsBusy() )return;
			OnRectAreaSelected( new Rectangle(0,0, m_Content.Width, m_Content.Height ) );
			OnSelectedToolChanged( ToolType.RectAreaSelect );
		}

		/// <summary>「選択解除」操作に対応する処理</summary>
		public void ClearSelection()
		{
			if( m_CurrState.IsBusy() )return;
			if( m_CurrState.ClearSelection() ){	DiscardCurrSelection();	}
		}

		/// <summary>「Copy」操作に対応する処理</summary>
		public void Copy(){	m_CurrState.Copy(false);	}

		/// <summary>「Cut」操作に対応する処理</summary>
		public void Cut(){	m_CurrState.Copy(true);	}

		/// <summary>「Paste」操作に対応する処理</summary>
		public void Paste()
		{
			if( m_CurrState.IsBusy() )return;

			var BMP = Util.GetBMP_from_Clipboard();
			if( BMP == null )return;

			m_CurrState.PrePaste();

			//貼りつける画像が現在の画像サイズより大きい場合，画像サイズを変更する
			if( BMP.Width>m_Content.Width || BMP.Height>m_Content.Height )
			{
				this.ChangeImgSizeTo( Math.Max(BMP.Width,m_Content.Width), Math.Max(BMP.Height,m_Content.Height) );
			}

			OnSelectedToolChanged( m_IView.IsRectModeSelectedForSelectionTool()  ?  ToolType.RectAreaSelect  :  ToolType.FreeFormAreaSelect );
			m_ImgFloatingState.Setup( CvtToImgPoint( m_IView.VisibleTopLeftOfImgView() ), BMP, null );
			ChangeCurrStateTo( m_ImgFloatingState );

			m_IView.OnImgPainted();
			m_IView.OnSelectionStateChanged(true);
		}

		#endregion
		//-----------------------------------
		#region Undo/Redo系 public

		/// <summary>Undo</summary>
		public void Undo()
		{
			if( !CanUndo() )return;

			m_CurrState.PreUndo();
			DiscardCurrSelection();
			EditTypes WhatDone = EditTypes.None;

			int iStart = ( (m_iHistoryPointer-1) / SNAP_SHOT_STEPS ) * SNAP_SHOT_STEPS;
			for( int i=iStart; i<m_iHistoryPointer; ++i )
			{	WhatDone |= m_EditHistory[i].Edit( m_Content );	}

			--m_iHistoryPointer;

			UpdateView( WhatDone );
			
		}

		/// <summary>Undoを実施可能か否か</summary>
		/// <returns></returns>
		public bool CanUndo()
		{
			if( m_CurrState.IsBusy() )return false;
			if( m_iHistoryPointer<=0 )return false;	//Undoすべき処理が無い
			return true;
		}

		/// <summary>Redo</summary>
		public void Redo()
		{
			if( !CanRedo() )return;
			DiscardCurrSelection();

			++m_iHistoryPointer;
			UpdateView( m_EditHistory[m_iHistoryPointer].Edit( m_Content ) );
		}

		/// <summary>Redoを実施可能か否か</summary>
		/// <returns></returns>
		public bool CanRedo()
		{
			if( m_CurrState.IsBusy() )return false;
			if( m_iHistoryPointer >= m_EditHistory.Count-1 )return false;	//Redoすべき処理が無い
			return true;
		}

		/// <summary>
		/// 既存の Undo/Redo 用の履歴データを全て破棄する．
		///		<remarks>
		///		※現在，この処理からはView側への表示更新要求が発生しないので注意．
		///		（：View側からこれを呼んだら，自主的にGUI更新をする必要がある）
		///		</remarks>
		/// </summary>
		public void DiscardEditHistoryData()
		{
			m_EditHistory.Clear();
			m_iHistoryPointer = 0;
			m_EditHistory.Add( new ChangeImg( m_Content.CreateCurrImgClone() ) );
		}

		#endregion
		//-----------------------------------
		#region その他 public

		/// <summary>
		/// 新規作成やファイル読込直後の状態か否か．
		/// View側からの状態問い合わせ用．
		///		<remarks>
		///		想定用途は，
		///		「別のファイルのロードやAPPの終了時等に確認を出すべきかどうかを判断する用」である．
		///		「画像の内容が初期と同一か？」ではないので，判断基準としては今一つな情報だが……
		///		</remarks>
		/// </summary>
		public bool IsImageJustAfterInitialized{	get;	private set;	}

		/// <summary>左右ボタンの色をSWAP</summary>
		public void SwapLRColor()
		{
			var Tmp = m_DrawColor[0];
			m_DrawColor[0] = m_DrawColor[1];
			m_DrawColor[1] = Tmp;
			m_IView.OnSelectedColorChanged( 0, m_DrawColor[0] );
			m_IView.OnSelectedColorChanged( 1, m_DrawColor[1] );

			OnRColorChanged();
		}

		/// <summary>現在の左ボタンの色</summary>
		public Color CurrLColor => m_DrawColor[0];
		/// <summary>現在の右ボタンの色</summary>
		public Color CurrRColor => m_DrawColor[1];

		#endregion
		//-----------------------------------
		#region IToolFormOpListener Impl

		/// <inheritdoc/>
		public void OnSelectedToolChanged( ToolType SelectedType )
		{
			if( m_CurrSelToolType == SelectedType )return;

			m_CurrSelToolType = SelectedType;
			m_IView.OnToolSelectionChangedTo( SelectedType );
			m_CurrState.OnToolSelected( m_CurrSelToolType );
		}

		/// <inheritdoc/>
		public void OnTransBackModeChanged( bool Trans )
		{
			m_ImgFloatingState.IsTransMode = Trans;
			if( m_CurrState==m_ImgFloatingState )
			{	m_IView.OnImgPainted();	}
		}

		/// <inheritdoc/>
		public void OnEraserSizeChanged( int Size )
		{	m_NormalState.OnEraserToolSizeChanged( Size );	}

		#endregion
		//-----------------------------------
		#region IColorFormOpListener Impl

		/// <inheritdoc/>
		public void OnColorSelected( int iDrawColor, System.Drawing.Color Col )
		{
			m_DrawColor[iDrawColor] = Col;
			m_IView.OnSelectedColorChanged( iDrawColor, Col );

			if( iDrawColor == 1 )OnRColorChanged();
		}

		#endregion
		//-----------------------------------
		#region private

		/// <summary>右ボタンの色が変わったときに行うべき透過色関係の内部処理</summary>
		private void OnRColorChanged()
		{
			m_ImgFloatingState.TransColor = m_DrawColor[1];

			if( m_CurrState==m_ImgFloatingState && m_ImgFloatingState.IsTransMode )
			{	m_IView.OnImgPainted();	}
		}

		/// <summary>引数オブジェクトでの編集を実施し，表示や編集履歴を更新する</summary>
		/// <param name="Editor">
		/// 編集処理実施者．
		/// ※この参照がそのまま編集履歴に保持され得る点に注意
		/// </param>
		private void EditWith( IEdit Editor )
		{
			EditTypes WhatDone = ( Editor!=null  ?   Editor.Edit( m_Content )  :  EditTypes.None );

			if( WhatDone != EditTypes.None )
			{
				AddToEditHistory( Editor );
				IsImageJustAfterInitialized = false;
				UpdateView( WhatDone );
			}
		}

		/// <summary>画像編集結果に応じたView更新</summary>
		/// <param name="WhatDone">編集内容</param>
		private void UpdateView( EditTypes WhatDone )
		{
			if( WhatDone.HasFlag( EditTypes.ImgSizeChange ) )
			{	m_IView.OnImgSizeChanged();	}
			else if( WhatDone.HasFlag( EditTypes.Draw ) )
			{	m_IView.OnImgPainted();	}
		}

		/// <summary>何か編集を新たに行った際の Undo/Redo 用編集履歴更新 </summary>
		/// <param name="edit">
		/// 実施した編集処理
		/// ※この参照がそのまま編集履歴に保持され得る点に注意
		/// </param>
		private void AddToEditHistory( IEdit edit )
		{
			++m_iHistoryPointer;

			//Redo可能な物がある状態で新しい編集を行った場合，Redo用データは全て破棄する
			if( m_iHistoryPointer < m_EditHistory.Count )
			{	m_EditHistory.RemoveRange( m_iHistoryPointer, m_EditHistory.Count - m_iHistoryPointer );	}

			//一定回数ごとに画像全体を履歴とし，Undo時の再生開始地点とする
			if( m_iHistoryPointer % SNAP_SHOT_STEPS == 0 )
			{	m_EditHistory.Add( new ChangeImg( m_Content.CreateCurrImgClone() ) );	}
			else
			{	m_EditHistory.Add( edit );	}
		}

		/// <summary>表示画像上のピクセル座標 → 画像データの座標（拡大率の逆数を乗じる）</summary>
		/// <param name="ViewPoint">表示画像上の座標</param>
		/// <returns>引数に対応した画像データの座標</returns>
		private Point CvtToImgPoint( Point ViewPoint )
		{
			int Rate = ViewMagnificationRate;
			return new Point( ViewPoint.X/Rate, ViewPoint.Y/Rate );
		}

		/// <summary>新しく矩形領域が選択された際の処理</summary>
		/// <param name="SelectedRect"></param>
		private void OnRectAreaSelected( Rectangle SelectedRect )
		{
			m_NewAreaSelectedState.SetupAsRect( SelectedRect );
			ChangeCurrStateTo( m_NewAreaSelectedState );
			m_IView.OnSelectionStateChanged(true);
			m_IView.UpdateSizeInfoView( SelectedRect.Size );
		}

		/// <summary>新しく自由形状な範囲が選択された際の処理</summary>
		/// <param name="FreeFromVtxs">選択範囲形状の頂点系列．3点以上のデータであること</param>
		private void OnFreeFormAreaSelected( Point[] FreeFromVtxs )
		{
			m_NewAreaSelectedState.SetupAsFreeForm( FreeFromVtxs );
			ChangeCurrStateTo( m_NewAreaSelectedState );
			m_IView.OnSelectionStateChanged(true);
			m_IView.UpdateSizeInfoView( Util.BoundingRect(FreeFromVtxs).Size );
		}

		/// <summary>内部ステート切替処理</summary>
		/// <param name="NewState"></param>
		private void ChangeCurrStateTo( IState NewState )
		{
			if( m_CurrState == NewState )return;
			m_CurrState?.PreLeave();
			m_CurrState = NewState;
			m_CurrState.OnEnter();
		}

		/// <summary>現在の範囲選択を破棄，ノーマルステートに遷移する</summary>
		private void DiscardCurrSelection()
		{
			m_NewAreaSelectedState.Clear();
			m_ImgFloatingState.Clear();
			ChangeCurrStateTo( m_NormalState );
			m_NormalState.OnToolSelected( m_CurrSelToolType );
			m_IView?.OnSelectionStateChanged( false );
		}

		/// <summary>
		/// グリッドの描画作業．
		/// ただし，グリッド表示しない設定状態である場合や，
		/// 拡大率が一定よりも小さい場合には描画しない．
		/// </summary>
		/// <param name="g">描画対象</param>
		private void DrawGrid( Graphics g )
		{
			if( !GridVisible || m_ViewMagRate < MinViewMagnificationRate_for_GridToBeShown )return;

			using( var Pen = new Pen( GridColor ) )
			{
				int mh = m_Content.Height * m_ViewMagRate;
				for( int x=m_GridSize.Width; x<m_Content.Width; x+=m_GridSize.Width )
				{
					int mx = x * m_ViewMagRate;
					g.DrawLine( Pen, mx,0, mx,mh );
				}
				int mw = m_Content.Width * m_ViewMagRate;
				for( int y=m_GridSize.Height; y<m_Content.Height; y+=m_GridSize.Height )
				{
					int my = y * m_ViewMagRate;
					g.DrawLine( Pen, 0,my, mw,my );
				}
			}
		}

		/// <summary>ファイルパス名（の拡張子部分）から，ファイルフォーマットを決める</summary>
		/// <param name="FilePath">ファイルパス名</param>
		/// <returns>不明な場合はnull</returns>
		private static System.Drawing.Imaging.ImageFormat FileFormatFromPathName( string FilePath )
		{
			var CheckStr = FilePath.ToUpper();
			if( CheckStr.EndsWith( ".BMP" ) )return System.Drawing.Imaging.ImageFormat.Bmp;
			if( CheckStr.EndsWith( ".PNG" ) )return System.Drawing.Imaging.ImageFormat.Png;
			if( CheckStr.EndsWith( ".JPG" ) )return System.Drawing.Imaging.ImageFormat.Jpeg;
			return null;
		}

		#endregion

		//=============================================

		/// <summary>ステート</summary>
		private interface IState
		{
			/// <summary>このステートから別のステートに遷移する直前に呼ばれる</summary>
			void PreLeave();
			/// <summary>このステートに遷移した直後に呼ばれる</summary>
			void OnEnter();

			/// <summary>キー入力に対する処理の実施</summary>
			/// <param name="keys">入力キー情報</param>
			/// <returns>
			/// 処理を実施した（キー入力を消費した）場合にはtrueを返す．
			/// →trueを返した場合には，GUI側はキー入力に対応した動作を行わない．
			/// </returns>
			bool ProcessKeyInput( System.Windows.Forms.Keys keys );

			/// <summary>マウスボタン押下時</summary>
			/// <param name="ImgPos">画像データ上座標</param>
			/// <param name="button">ボタン</param>
			void OnMouseDown( Point ImgPos, System.Windows.Forms.MouseButtons button );

			/// <summary>マウスボタン解放時</summary>
			/// <param name="ImgPos">画像データ上座標</param>
			/// <param name="button">ボタン</param>
			void OnMouseUp( Point ImgPos, System.Windows.Forms.MouseButtons button );

			/// <summary>マウス移動時</summary>
			/// <param name="ImgPos">画像データ上座標</param>
			void OnMouseMove( Point ImgPos );

			/// <summary>画像表示域ではない箇所（外側の何もない箇所）がクリックされた際</summary>
			/// <param name="button">ボタン</param>
			void OnOutsidePosClicked( System.Windows.Forms.MouseButtons button );

			/// <summary>画像表示域の内容を描画する</summary>
			/// <param name="g">画像表示域に描画するための Graphics</param>
			void DrawCurrentView( Graphics g );

			/// <summary>サムネイル表示域の内容を描画する</summary>
			/// <param name="g">サムネイル表示域に描画するための Graphics</param>
			void DrawThumbnail( Graphics g );

			/// <summary>
			/// 現在処理途中か否か．
			/// Undo/Redoや全選択,Pasteのような処理を禁止する用</summary>
			/// <returns>trueを返す場合，キーやメニューによる操作が棄却される</returns>
			bool IsBusy();

			/// <summary>「選択解除」操作(DELキー)が成された際の処理</summary>
			/// <returns>「選択解除」を実施したか否か</returns>
			bool ClearSelection();

			/// <summary>ツールの選択が変更された際の処理</summary>
			/// <param name="SelectedTool">選択されたツール</param>
			void OnToolSelected( ToolType SelectedTool );

			/// <summary>「コピー」または「切り取り」操作が成された際の処理</summary>
			/// <param name="IsCut">操作種類：trueなら「切り取り」，falseなら「コピー」</param>
			void Copy( bool IsCut );

			/// <summary>「ペースト」が成される直前の処理</summary>
			void PrePaste();
			/// <summary>「Undo」が成される直前の処理</summary>
			void PreUndo();

			/// <summary>
			/// 選択範囲の画像を生成する．
			/// （選択範囲をファイルに出力する機能用）
			/// </summary>
			/// <returns>選択範囲の画像．選択範囲が無い場合にはnull</returns>
			Bitmap CreateSelectedAreaImg();

			/// <summary>「反転/回転」操作が成された際の処理</summary>
			/// <param name="ActType">操作により選択された処理</param>
			void RotateFlip( RotateFlipType ActType );
		}

		//=============================================

		/// <summary>
		/// ノーマルステート：
		/// * 範囲選択が成されていない状態
		/// * 各種ツールでの編集作業を実施するステート
		/// </summary>
		private class NormalState : IState
		{
			private Presenter m_Owner;
			private Bitmap m_EditImg;	//画像編集用バッファ
			private ITool m_CurrTool = null;

			//
			private ColorPickTool m_ColorPickTool;
			private PenTool m_PenTool = new PenTool();
			private BrushTool m_BrushTool = new BrushTool();
			private LineTool m_LineTool = new LineTool();
			private EraserTool m_EraserTool = new EraserTool();
			private RectSelTool m_RectSelTool = new RectSelTool();
			private FreeFormSelTool m_FreeFormSelTool = new FreeFormSelTool();
			private FillTool m_FillTool = new FillTool();

			/// <summary>ctor</summary>
			/// <param name="Owner"></param>
			public NormalState( Presenter Owner )
			{	
				m_Owner = Owner;
				m_ColorPickTool = new ColorPickTool( Owner );
				m_CurrTool = m_PenTool;

				m_RectSelTool.ShowDraggingAreaSize += (Size size)=>{	Owner.m_IView.UpdateSizeInfoView(size);	};
				m_RectSelTool.OnRectAreaSelected += Owner.OnRectAreaSelected;

				m_FreeFormSelTool.OnFreeFormAreaSelected += Owner.OnFreeFormAreaSelected;
			}

			/// <summary>
			/// Eraserルールのサイズが変更されたとき
			/// </summary>
			/// <param name="Size">サイズ．(e.g. 3x3ならば3</param>
			public void OnEraserToolSizeChanged( int Size )
			{
				m_EraserTool.ChangeSize( Size );
				if( m_CurrTool.Type == ToolType.Eraser )
				{	m_Owner.m_IView?.OnImgPainted();	}
			}

			//--------------
			#region IState Impl

			public void PreLeave(){	Util.DisposeBMP( ref m_EditImg );	}
			public void OnEnter(){	/*NOP*/	}
			public void OnOutsidePosClicked( System.Windows.Forms.MouseButtons button ){	/*NOP*/	}

			//編集処理中はGUIのキーによる操作を抑制
			public bool ProcessKeyInput( System.Windows.Forms.Keys keys ){	return IsBusy();	}

			//
			public void OnMouseDown( Point ImgPos, System.Windows.Forms.MouseButtons button )
			{
				if( !m_CurrTool.IsBusy() )
				{//※操作開始時
					if( !SetupTool( m_Owner.m_CurrSelToolType, button, m_Owner ) )
					{	return;	}
					
					Util.DisposeBMP( ref m_EditImg );
					m_EditImg = m_Owner.m_Content.CreateCurrImgClone();
					
				}
				//マウス押下時処理の実施
				HandleToolResult( m_CurrTool.OnMouseDown( ImgPos, button, m_EditImg ) );
			}

			//
			public void OnMouseUp( Point ImgPos, System.Windows.Forms.MouseButtons button )
			{	HandleToolResult( m_CurrTool.OnMouseUp(ImgPos, button, m_EditImg) );	}

			//
			public void OnMouseMove( Point ImgPos )
			{	HandleToolResult( m_CurrTool.OnMouseMove(ImgPos, m_EditImg) );	}

			//
			public void DrawCurrentView( Graphics g )
			{
				int MagRate = m_Owner.ViewMagnificationRate;

				if( m_CurrTool.IsBusy() )
				{	Util.DrawMagnifiedImg( g, m_EditImg, MagRate );	}
				else
				{	m_Owner.m_Content.DrawMagnifiedImgTo( g, MagRate );	}

				m_Owner.DrawGrid( g );
				m_CurrTool.DrawStateToViewImg( g, MagRate );
			}

			//
			public void DrawThumbnail( Graphics g )
			{
				if( m_CurrTool.IsBusy() )
				{	Util.DrawMagnifiedImg( g, m_EditImg, 1 );	}
				else
				{	m_Owner.m_Content.DrawMagnifiedImgTo( g, 1 );	}
			}

			//
			public bool IsBusy(){	return  m_CurrTool.IsBusy();	}
			public bool ClearSelection(){	return false;	}
			public void OnToolSelected( ToolType SelectedTool )
			{
				if( IsBusy() )throw new InvalidOperationException( "Tool Changed During Busy!!");
				
				switch( SelectedTool )
				{
				case ToolType.Pen:	m_CurrTool = m_PenTool;	break;
				case ToolType.Brush:	m_CurrTool = m_BrushTool;	break;
				case ToolType.Line:	m_CurrTool = m_LineTool;	break;
				case ToolType.Eraser:
					m_EraserTool.Setup( m_Owner.m_DrawColor[0], m_Owner.m_DrawColor[1], m_Owner.m_IView.GetEraserToolSize() );
					m_CurrTool = m_EraserTool;
					break;
				case ToolType.Fill:	m_CurrTool = m_FillTool;	break;
				case ToolType.RectAreaSelect:	m_CurrTool = m_RectSelTool;	break;
				case ToolType.FreeFormAreaSelect:	m_CurrTool = m_FreeFormSelTool;	break;
				}

				m_Owner.m_IView?.OnImgPainted();
			}

			public void Copy( bool IsCut ){	/*NOP*/	}
			public void PrePaste(){	/*NOP*/	}
			public void PreUndo(){	/*NOP*/	}
			public Bitmap CreateSelectedAreaImg(){	return null;	}

			public void RotateFlip( RotateFlipType ActType )
			{
				if( IsBusy() )return;

				//※ここにくるとき，範囲選択はなされていないハズ．
				//この場合，画像全体を処理対象とする．
				var NewImg = m_Owner.m_Content.CreateCurrImgClone();
				NewImg.RotateFlip( ActType );
				m_Owner.EditWith(  new ChangeImg( NewImg ) );
			}

			#endregion
			//--------------
			#region private

			/// <summary>
			/// スポイトツール終了時コールバック用．
			/// ツール選択状態をスポイト実施直前の物に戻す．
			/// </summary>
			/// <param name="ToolToBeSelected">選択し直すべきツール</param>
			private void OnColorPickerFinished( ToolType ToolToBeSelected )
			{	OnToolSelected( ToolToBeSelected );	}

			/// <summary>
			/// マウスボタンが押された際の作業関数．使用すべきツールオブジェクトをセットアップし，m_CurrToolに選択．
			/// </summary>
			/// <param name="CurrSelTool">現在GUIで選択されているツール種類</param>
			/// <param name="button">押されたボタン</param>
			/// <param name="Owner">Presenter</param>
			/// <returns>成否．使用すべきツールが無い場合には false．</returns>
			private bool SetupTool( ToolType CurrSelTool, System.Windows.Forms.MouseButtons button, Presenter Owner )
			{
				int iDrawColor = Util.DrawColorIndexFor( button );
				if( iDrawColor<0 )return false;

				//ツールによってはCtrlキー押下している際にはスポイトツールになる
				if( 
					System.Windows.Forms.Control.ModifierKeys.HasFlag( System.Windows.Forms.Keys.Control ) &&
					Tool.ShouldActAsColorPicker_when_Ctrl( CurrSelTool )
				)
				{
					m_ColorPickTool.Setup( this.OnColorPickerFinished, CurrSelTool );
					m_CurrTool = m_ColorPickTool;
					m_Owner.m_IView.OnImgPainted();	//直前のツールが平時に何かを描くタイプだった場合にその表示を消す
					return true;
				}

				switch( CurrSelTool )
				{
				case ToolType.Pen:
					m_PenTool.Setup( Owner.m_DrawColor[iDrawColor] );
					m_CurrTool = m_PenTool;
					return true;
				case ToolType.Brush:
					m_BrushTool.Setup( Owner.m_DrawColor[iDrawColor] );
					m_CurrTool = m_BrushTool;
					return true;
				case ToolType.Line:
					m_LineTool.Setup( Owner.m_DrawColor[iDrawColor], Owner.m_IView.CreateLineToolSetting() );
					m_CurrTool = m_LineTool;
					return true;
				case ToolType.RectAreaSelect:
					m_CurrTool = m_RectSelTool;
					return true;
				case ToolType.FreeFormAreaSelect:
					m_CurrTool = m_FreeFormSelTool;
					return true;
				case ToolType.Eraser:
					m_EraserTool.Setup( Owner.m_DrawColor[0], Owner.m_DrawColor[1], Owner.m_IView.GetEraserToolSize() );
					m_CurrTool = m_EraserTool;
					return true;
				case ToolType.Fill:
					m_FillTool.Setup( Owner.m_DrawColor[iDrawColor] );
					m_CurrTool = m_FillTool;
					return true;
				default:
					return false;
				}
			}

			/// <summary>現ツールでのマウス操作結果を処理</summary>
			/// <param name="Result">操作結果</param>
			private void HandleToolResult( ToolProcResult Result )
			{
				switch( Result )
				{
				case ToolProcResult.ShouldUpdateView:
					m_Owner.m_IView.OnImgPainted();
					break;
				case ToolProcResult.EditCompleted:
					{
						//編集バッファの内容が現在の画像データと同一なら編集自体を無かったことにする
						if( m_Owner.m_Content.IsSame( m_EditImg ) )
						{	m_Owner.m_IView.OnImgPainted();	}
						else
						{	m_Owner.EditWith( m_CurrTool.CreateEdit() );	}
					}
					break;
				case ToolProcResult.EditShouldBeRejected:
					m_Owner.m_IView.OnImgPainted();
					break;
				default:
					break;
				}
			}

			#endregion
		}

		//=============================================

		/// <summary>
		/// 範囲選択が成された際のステート：
		/// * 範囲選択系ツール（あるいは全選択操作）で選択範囲が決定した直後のステート
		/// </summary>
		private class NewAreaSelectedState : IState
		{
			private Presenter m_Owner;

			//範囲
			private Rectangle m_AABB;	//自由形状時にはその形状のAABB，矩形範囲時はその範囲そのものとする
			private Point[] m_FreeFormVtxs;	//自由形状時にはその形状の頂点群．そうでない場合にはnullとする

			//ctor
			public NewAreaSelectedState( Presenter Owner ){	m_Owner = Owner;	}

			/// <summary>
			/// 選択範囲のセットアップ（矩形範囲選択時）
			/// </summary>
			/// <param name="SelectedArea">選択範囲</param>
			public void SetupAsRect( Rectangle SelectedArea )
			{
				m_FreeFormVtxs = null;
				m_AABB = SelectedArea;
			}

			/// <summary>
			/// 選択範囲のセットアップ（自由形状時）
			/// </summary>
			/// <param name="FreeFormVtxs">形状の頂点系列</param>
			public void SetupAsFreeForm( IReadOnlyList<Point> FreeFormVtxs )
			{
				m_FreeFormVtxs = FreeFormVtxs.ToArray();
				m_AABB = Util.BoundingRect( m_FreeFormVtxs );
			}

			/// <summary>
			/// 未セットアップ状態にする
			/// </summary>
			public void Clear()
			{
				m_FreeFormVtxs = null;
				m_AABB = new Rectangle();
			}

			//--------------
			#region IState Impl

			public void PreLeave(){	m_Owner.m_IView?.ChangeImgViewCursorTo( System.Windows.Forms.Cursors.Default );	}
			public void OnEnter(){	/*NOP*/	}

			public bool ProcessKeyInput( System.Windows.Forms.Keys keys )
			{
				if( m_AABB.IsEmpty )return false;

				//矢印キーを処理する
				int dx=0;
				int dy=0;
				var KeyCode = ( keys & Keys.KeyCode );
				if( KeyCode == Keys.Left ){	--dx;	}
				if( KeyCode == Keys.Right ){	++dx;	}
				if( KeyCode == Keys.Up ){	--dy;	}
				if( KeyCode == Keys.Down ){	++dy;	}
				if( dx==0 && dy==0 )return false;

				var Modifier = ( keys & Keys.Modifiers );

				if( IsFreeFormMode )
				{//※MEMO : 現状の範囲表示方法では自由形状範囲を動かしても意味不明であるから何もしない．
					//ただしキー入力は消費したことにする
					return true;
				}
				else
				{//矩形モード時
					if( Modifier==Keys.Control || Modifier==Keys.Shift )
					{//Ctrl or Shift + 矢印 で範囲サイズを変更
						if( dx<0 && (m_AABB.Width<=1) ){	dx = 0;	}
						if( dx>0 ){	dx = Math.Min( dx, m_Owner.ImgWidth-m_AABB.Right );	}
						if( dy<0 && (m_AABB.Height<=1) ){	dy = 0;	}
						if( dy>0 ){	dy = Math.Min( dy, m_Owner.ImgHeight-m_AABB.Bottom );	}

						if( dx!=0 || dy!=0 )
						{
							m_AABB.Width += dx;
							m_AABB.Height += dy;
							m_Owner.m_IView.OnImgPainted();
							m_Owner.m_IView.UpdateSizeInfoView( m_AABB.Size );
						}
					}
					else
					{//範囲を平行移動
						if( OffsetRegion( dx,dy ) )
						{	m_Owner.m_IView.OnImgPainted();	}
					}
				}
				return true;
			}

			//
			public void OnMouseDown( Point ImgPos, System.Windows.Forms.MouseButtons button )
			{
				if( !button.HasFlag( System.Windows.Forms.MouseButtons.Left ) )return;

				if( m_AABB.Contains( ImgPos ) )
				{//AABB範囲内ならドラッグ開始
					bool IsCopyMode = System.Windows.Forms.Control.ModifierKeys.HasFlag( System.Windows.Forms.Keys.Control );

					if( IsFreeFormMode )
					{
						m_Owner.m_ImgFloatingState.Setup(
							new Point( m_AABB.X, m_AABB.Y ),
							m_Owner.m_Content.CreatePartialImg( FreeFormPath ),
							( IsCopyMode  ?  null  :  FreeFormPath ) 
						);
					}
					else
					{
						if( IsCopyMode )
						{
							m_Owner.m_ImgFloatingState.Setup( new Point( m_AABB.X, m_AABB.Y ), m_Owner.m_Content.CreatePartialImg( m_AABB ), null );
						}
						else
						{
							var RegPath = new System.Drawing.Drawing2D.GraphicsPath();
							RegPath.AddRectangle( m_AABB );
							m_Owner.m_ImgFloatingState.Setup( new Point( m_AABB.X, m_AABB.Y ), m_Owner.m_Content.CreatePartialImg( m_AABB ), m_AABB );
						}
					}
					
					m_Owner.ChangeCurrStateTo( m_Owner.m_ImgFloatingState );
				}
				else
				{//範囲外の場合は現在の範囲選択状態を破棄
					m_Owner.DiscardCurrSelection();
				}
				//※このマウス押下がドラッグ開始操作処理となり得るので遷移先ステートに処理を移譲する
				m_Owner.m_CurrState.OnMouseDown( ImgPos, button );
			}

			//
			public void OnMouseUp( Point ImgPos, System.Windows.Forms.MouseButtons button ){	/*NOP*/	}

			//
			public void OnMouseMove( Point ImgPos )
			{	//カーソルの変更
				m_Owner.m_IView?.ChangeImgViewCursorTo(
					m_AABB.Contains( ImgPos ) ? System.Windows.Forms.Cursors.SizeAll : System.Windows.Forms.Cursors.Default
				);
			}

			//画像表示領域よりも外側をクリックする操作→範囲選択状態を破棄
			public void OnOutsidePosClicked( System.Windows.Forms.MouseButtons button )
			{	m_Owner.DiscardCurrSelection();	}

			//
			public void DrawCurrentView( Graphics g )
			{//AABBを描画
				int MagRate = m_Owner.ViewMagnificationRate;
				m_Owner.m_Content.DrawMagnifiedImgTo( g, MagRate );
				m_Owner.DrawGrid( g );

				if( IsFreeFormMode )
				{
					using( var path = FreeFormPath )
					{
						if( path == null )return;

						using( var Pen1 = new Pen( new System.Drawing.Drawing2D.HatchBrush( System.Drawing.Drawing2D.HatchStyle.Percent50, Color.Gray, Color.Transparent ) ) )
						using( var Pen2 = new Pen( new System.Drawing.Drawing2D.HatchBrush( System.Drawing.Drawing2D.HatchStyle.Percent10, Color.LightGray, Color.Transparent ) ) )
						{
							g.ScaleTransform( MagRate,MagRate );
							g.TranslateTransform( 0.5f, 0.5f );	//※何故か半画素ずれるので補正
							g.DrawPath( Pen1, path );
							g.DrawPath( Pen2, path );
							g.ResetTransform();
						}
					}
				}

				Util.DrawRectSelectionState( g, m_AABB, MagRate, true );
			}

			public void DrawThumbnail( Graphics g ){	m_Owner.m_Content.DrawMagnifiedImgTo( g, 1 );	}
			public bool IsBusy(){	return false;	}

			public bool ClearSelection()
			{//DELキー操作→画像の選択範囲を削除（＝背景色で塗りつぶす）
				if( IsFreeFormMode )
				{	m_Owner.EditWith( new FillPath( FreeFormPath, m_Owner.m_DrawColor[1] ) );	}
				else if( !m_AABB.IsEmpty )
				{	m_Owner.EditWith( new FillRect( m_AABB, m_Owner.m_DrawColor[1] ) );	}

				return true;
			}

			//このステートに入った際のツールとは別のツールが選択されたらその時点で現在の範囲選択状態は破棄
			public void OnToolSelected( ToolType SelectedTool )
			{
				if( IsFreeFormMode &&  SelectedTool==ToolType.FreeFormAreaSelect )return;
				if( !IsFreeFormMode  &&  SelectedTool==ToolType.RectAreaSelect )return;

				m_Owner.DiscardCurrSelection();
			}

			public void Copy( bool IsCut )
			{
				if( IsBusy() || m_AABB.IsEmpty )return;

				if( IsFreeFormMode )
				{
					using( var Img = m_Owner.m_Content.CreatePartialImg( FreeFormPath ) )
					{	Util.CopyBMP_To_Clipboard( Img );	}
				}
				else
				{
					using( var Img = m_Owner.m_Content.CreatePartialImg( m_AABB ) )
					{	Util.CopyBMP_To_Clipboard( Img );	}
				}

				if( IsCut )
				{
					ClearSelection();
					m_Owner.DiscardCurrSelection();
				}
			}

			public void PrePaste(){	/*NOP*/	}
			public void PreUndo(){	/*NOP*/	}
			public Bitmap CreateSelectedAreaImg(){	return m_Owner.m_Content.CreatePartialImg( m_AABB );	}

			public void RotateFlip( RotateFlipType ActType )
			{
				if( m_AABB.IsEmpty )return;

				if( IsFreeFormMode )
				{
					//選択範囲を引数に応じて変形させた画像が浮いている状態に遷移する
					var FloatingImg = m_Owner.m_Content.CreatePartialImg( FreeFormPath );
					FloatingImg.RotateFlip( ActType );

					m_Owner.m_ImgFloatingState.Setup(
							new Point( m_AABB.X, m_AABB.Y ), //※回転時の座標はどうする？
							FloatingImg,
							null
						);
					m_Owner.ChangeCurrStateTo( m_Owner.m_ImgFloatingState );

					//変形前の選択範囲領域を背景色で塗りつぶす
					m_Owner.EditWith( new FillPath( FreeFormPath, m_Owner.m_DrawColor[1] ) );
				}
				else
				{
					//選択範囲を引数に応じて変形させた画像が浮いている状態に遷移する
					var FloatingImg = m_Owner.m_Content.CreatePartialImg( m_AABB );
					FloatingImg.RotateFlip( ActType );

					m_Owner.m_ImgFloatingState.Setup(
							new Point( m_AABB.X, m_AABB.Y ), //※回転時の座標はどうする？
							FloatingImg,
							null
						);
					m_Owner.ChangeCurrStateTo( m_Owner.m_ImgFloatingState );

					//変形前の選択範囲領域を背景色で塗りつぶす
					m_Owner.EditWith( new FillRect( m_AABB, m_Owner.m_DrawColor[1] ) );
				}
			}

			#endregion
			//--------------
			#region private

			//自由形状を扱っている状態化否か．falseなら矩形．
			private bool IsFreeFormMode => (m_FreeFormVtxs!=null);

			//m_FreeFormVtxs の形状に相当する GraphicsPath を生成して返す
			private System.Drawing.Drawing2D.GraphicsPath FreeFormPath
			{
				get
				{
					if( m_FreeFormVtxs==null )return null;
					var Path = new System.Drawing.Drawing2D.GraphicsPath();
					Path.AddPolygon( m_FreeFormVtxs );
					return Path;
				}
			}

			//選択領域を画像領域をはみ出さない範囲でオフセットする．オフセットしたか否かを返す．
			private bool OffsetRegion( int dx, int dy )
			{
				if( dx<0 ){	dx = Math.Max( -m_AABB.Left, dx );	}
				if( dx>0 ){	dx = Math.Min( dx, m_Owner.ImgWidth-m_AABB.Right );	}
				if( dy<0 ){	dy = Math.Max( -m_AABB.Top, dy );	}
				if( dy>0 ){	dy = Math.Min( dy, m_Owner.ImgHeight-m_AABB.Bottom );	}
				
				if( dx==0 && dy==0 )return false;
				m_AABB.Offset( dx, dy );
				if( m_FreeFormVtxs != null )
				{
					for( int i=0; i<m_FreeFormVtxs.Length; ++i )
					{	m_FreeFormVtxs[i].Offset(dx,dy);	}
				}
				return true;
			}

			#endregion
		}

		//=============================================

		/// <summary>
		/// ドラッグで動かせる画像が浮いているステート：
		/// 以下のような操作時．
		/// * 選択範囲をドラッグしたとき
		/// * Paste されたとき
		/// </summary>
		private class ImgFloatingState : IState
		{
			private Presenter m_Owner;

			//浮いている絵とその現在位置
			private Point m_TopLeft;
			private Bitmap m_FloatingImg;

			//初期範囲を背景色で塗りつぶす処理．塗りつぶしが不要な場合には null．
			private IEdit m_FillInitialArea = null;

			//ドラッグ操作用
			private bool m_IsDragging = false;
			private Point m_PrevMousePos;
			private Bitmap m_EditImg;

			//特定色を透過する用
			private bool m_IsTransMode = false;
			private Color m_TransCol = Color.White;
			private Bitmap m_TransModeImg;

			//ctor
			public ImgFloatingState( Presenter Owner ){	m_Owner = Owner;	}

			/// <summary>
			/// 開始準備
			/// </summary>
			/// <param name="FloatingTopLeft">浮いている絵の初期位置</param>
			/// <param name="FloatingImg">浮いている絵(この参照がそのまま保持される)</param>
			/// <param name="EraseReg">
			/// 背景色で塗りつぶすべき範囲．塗りつぶし不要であればnull．
			/// 想定用途：
			/// * 選択範囲をドラッグすることでこのステートに入る場合に指定する,
			///   （ドラッグによって画像内容が「移動」されたときに，元の場所の画像内容を消去するための情報）
			/// * Paste操作等でこのステートに入る場合にはnullとする．
			/// </param>
			public void Setup( Point FloatingTopLeft, Bitmap FloatingImg, System.Drawing.Drawing2D.GraphicsPath EraseReg )
			{
				m_TopLeft = FloatingTopLeft;
				m_FloatingImg = FloatingImg;
				m_IsDragging = false;
				if( IsTransMode )RecreateTransModeImg();

				Util.DisposeBMP( ref m_EditImg );
				if( EraseReg != null )
				{
					m_EditImg = m_Owner.m_Content.CreateCurrImgClone();

					var FR = new FillPath( EraseReg, m_Owner.m_DrawColor[1] );
					FR.Draw( m_EditImg );
					m_FillInitialArea = FR;
				}
				else
				{	m_FillInitialArea = null;	}
			}

			/// <summary>
			/// 開始準備
			/// </summary>
			/// <param name="FloatingTopLeft">浮いている絵の初期位置</param>
			/// <param name="FloatingImg">浮いている絵(この参照がそのまま保持される)</param>
			/// <param name="EraseRect">
			/// 背景色で塗りつぶすべき矩形範囲．
			/// 想定用途：
			/// * 矩形選択範囲をドラッグすることでこのステートに入る場合に指定する,
			///   （ドラッグによって画像内容が「移動」されたときに，元の場所の画像内容を消去するための情報）
			/// </param>
			public void Setup( Point FloatingTopLeft, Bitmap FloatingImg, Rectangle EraseRect )
			{
				m_TopLeft = FloatingTopLeft;
				m_FloatingImg = FloatingImg;
				m_IsDragging = false;
				if( IsTransMode )RecreateTransModeImg();

				Util.DisposeBMP( ref m_EditImg );

				m_EditImg = m_Owner.m_Content.CreateCurrImgClone();

				var FR = new FillRect( EraseRect, m_Owner.m_DrawColor[1] );
				FR.Draw( m_EditImg );
				m_FillInitialArea = FR;
			}

			/// <summary>未セットアップ状態にする</summary>
			public void Clear()
			{
				Util.DisposeBMP( ref m_EditImg );
				Util.DisposeBMP( ref m_FloatingImg );
				Util.DisposeBMP( ref m_TransModeImg );
				m_FillInitialArea = null;
			}

			/// <summary>指定色の部分を透過するか否か</summary>
			public bool IsTransMode
			{
				get{	return m_IsTransMode;	}
				set
				{
					if( m_IsTransMode == value )return;
					m_IsTransMode = value;
					if( m_IsTransMode && m_FloatingImg!=null )
					{	RecreateTransModeImg();	}
				}
			}

			/// <summary>透過色</summary>
			public Color TransColor
			{
				get{	return m_TransCol;	}
				set
				{
					if( m_TransCol.Equals( value ) )return;
					m_TransCol = value;
					if( m_IsTransMode && m_FloatingImg!=null )
					{	RecreateTransModeImg();	}
				}
			}

			//--------------
			#region IState Impl

			public void PreLeave()
			{
				Clear();
				m_Owner.m_IView?.ChangeImgViewCursorTo( System.Windows.Forms.Cursors.Default );
			}

			public void OnEnter()
			{	m_Owner.m_IView.UpdateSizeInfoView( m_FloatingImg.Size );	}

			//
			public bool ProcessKeyInput( System.Windows.Forms.Keys keys )
			{
				if( m_IsDragging )return true;

				//矢印キーを処理する
				int dx=0;
				int dy=0;
				var KeyCode = ( keys & Keys.KeyCode );
				if( KeyCode == Keys.Left ){	--dx;	}
				if( KeyCode == Keys.Right ){	++dx;	}
				if( KeyCode == Keys.Up ){	--dy;	}
				if( KeyCode == Keys.Down ){	++dy;	}
				if( dx==0 && dy==0 )return false;

				m_TopLeft.X += dx;
				m_TopLeft.Y += dy;
				m_Owner.m_IView.OnImgPainted();
				return true;
			}

			//
			public void OnMouseDown( Point ImgPos, System.Windows.Forms.MouseButtons button )
			{
				if( !button.HasFlag( System.Windows.Forms.MouseButtons.Left ) )return;

				if( CurrRect.Contains( ImgPos ) )
				{//範囲内ならドラッグ開始
					m_IsDragging = true;
					m_PrevMousePos = ImgPos;

					//Ctrlが押されている場合は複製（：現在の場所で確定し，すぐにまた始める）
					if( System.Windows.Forms.Control.ModifierKeys.HasFlag( System.Windows.Forms.Keys.Control ) )
					{
						var FloatingImg = (Bitmap)m_FloatingImg.Clone();	

						AcceptEdit();

						this.Setup(	m_TopLeft, FloatingImg, null );
						m_Owner.ChangeCurrStateTo( this );
						m_IsDragging = true;
					}
				}
				else	//範囲外の場合は現在位置に確定
				{
					AcceptEdit();
					//※このマウス押下がドラッグ開始操作処理となり得るので遷移先ステートに処理を移譲する
					m_Owner.m_CurrState.OnMouseDown( ImgPos, button );
				}
			}

			//
			public void OnMouseUp( Point ImgPos, System.Windows.Forms.MouseButtons button )
			{
				if( !button.HasFlag( System.Windows.Forms.MouseButtons.Left ) )return;
				m_IsDragging = false;
				m_Owner.m_IView.OnImgPainted();
			}

			//
			public void OnMouseMove( Point ImgPos )
			{
				if( m_IsDragging )
				{
					if( m_PrevMousePos.Equals(ImgPos) )return;
					m_TopLeft.X += ImgPos.X - m_PrevMousePos.X;
					m_TopLeft.Y += ImgPos.Y - m_PrevMousePos.Y;
					m_PrevMousePos = ImgPos;
					m_Owner.m_IView.OnImgPainted();
				}
				else
				{//カーソル変更
					m_Owner.m_IView?.ChangeImgViewCursorTo(
						CurrRect.Contains( ImgPos ) ? System.Windows.Forms.Cursors.SizeAll : System.Windows.Forms.Cursors.Default
					);
				}
			}

			//画像表示領域の外側をクリックする操作→現在の位置に確定
			public void OnOutsidePosClicked( System.Windows.Forms.MouseButtons button ){	AcceptEdit();	}

			//
			public void DrawCurrentView( Graphics g )
			{
				int MagRate = m_Owner.ViewMagnificationRate;
				if( m_EditImg != null )
				{	Util.DrawMagnifiedImg( g, m_EditImg, MagRate );	}
				else
				{	m_Owner.m_Content.DrawMagnifiedImgTo( g, MagRate );	}

				Util.DrawMagnifiedImg( g, ( IsTransMode ? m_TransModeImg : m_FloatingImg ), MagRate, m_TopLeft.X, m_TopLeft.Y );
				m_Owner.DrawGrid( g );
				Util.DrawRectSelectionState( g, CurrRect, MagRate, !m_IsDragging );
			}

			public void DrawThumbnail( Graphics g )
			{
				if( m_EditImg != null )
				{	Util.DrawMagnifiedImg( g, m_EditImg, 1 );	}
				else
				{	m_Owner.m_Content.DrawMagnifiedImgTo( g, 1 );	}

				Util.DrawMagnifiedImg( g, ( IsTransMode ? m_TransModeImg : m_FloatingImg ), 1, m_TopLeft.X, m_TopLeft.Y );
			}

			public bool IsBusy(){	return m_IsDragging;	}
			
			public bool ClearSelection()
			{//DELキー操作→浮いている絵は棄却するが，「移動」元の範囲は消去する
				if( m_FillInitialArea != null )
				{	m_Owner.EditWith( m_FillInitialArea );	}
	
				return true;
			}

			//何かツール選択操作が成された場合は，この場所に確定
			public void OnToolSelected( ToolType SelectedTool ){	AcceptEdit();	}

			public void Copy( bool IsCut )
			{
				if( IsBusy() || m_FloatingImg==null )return;
				Util.CopyBMP_To_Clipboard( m_FloatingImg );

				if( IsCut )
				{
					ClearSelection();
					m_Owner.DiscardCurrSelection();
				}
			}

			public void PrePaste(){	AcceptEdit();	}	//ペースト操作が成された場合，まずは現在の場所に確定
			public void PreUndo(){	AcceptEdit();	}	//Undo操作が成される直前に確定
			public Bitmap CreateSelectedAreaImg(){	return m_FloatingImg;	}

			public void RotateFlip( RotateFlipType ActType )
			{
				m_FloatingImg.RotateFlip( ActType );
				m_TransModeImg?.RotateFlip( ActType );
				m_Owner.m_IView.OnImgPainted();
			}

			#endregion
			//--------------
			#region private

			/// <summary>現在の浮いている画像の範囲</summary>
			private Rectangle CurrRect => new Rectangle( m_TopLeft, m_FloatingImg.Size );

			/// <summary>
			/// 現在の位置に確定：浮いている絵を画像データに書込む
			/// </summary>
			private void AcceptEdit()
			{
				m_IsDragging = false;

				var ImgPainter = new DrawImg( new Bitmap( IsTransMode ? m_TransModeImg : m_FloatingImg ), m_TopLeft );
				if( m_FillInitialArea != null )
				{//移動元の消去とまとめて１回の編集操作（：Undo/Redoの単位）とする
					var Edits = new EditSeq();
					Edits.Add( m_FillInitialArea ).Add( ImgPainter );
					m_Owner.EditWith( Edits );
				}
				else
				{	m_Owner.EditWith( ImgPainter );	}

				m_Owner.DiscardCurrSelection();
			}

			/// <summary>特定色範囲を透過する用画像データの再生成</summary>
			private void RecreateTransModeImg()
			{
				Util.DisposeBMP( ref m_TransModeImg );
				m_TransModeImg = (Bitmap)m_FloatingImg.Clone();//new Bitmap( m_FloatingImg );
				m_TransModeImg.MakeTransparent( m_TransCol );
			}

			#endregion
		}
	}
}
