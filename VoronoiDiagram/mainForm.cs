/* $LAN=C#$ */

/*
 * 版權宣告
 * 學號:M113040108
 * 姓名:陳其謙 
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using VoronoiDiagram.Models;
using VoronoiDiagram.Functions;
using System.IO;
using System.Security;
using System.Text.RegularExpressions;
using System.Threading;

namespace VoronoiDiagram
{
    public partial class mainForm : Form
    {
        bool isReadFile = false; //用於確定有讀取檔案才能做 next test button
        List<PointF> lst_point = new List<PointF>();  //點集合
        List<Edge> lst_edge = new List<Edge>();       //邊集合
        Voronoi v = new Voronoi();

        private Bitmap bmp;
        private OpenFileDialog openFileDialog;
        StreamReader sr_file;
        Thread _trd;

        //委派方法才能跨執行緒
        private delegate void DeladdP(PointF p);
        private delegate void DelInit();
        private delegate void DeldrawE(PointF p1, PointF p2, Color color);
        //執行緒狀態
        ManualResetEvent _shutdownEvent = new ManualResetEvent(false);
        ManualResetEvent _pauseEvent = new ManualResetEvent(true);

        public mainForm()
        {
            InitializeComponent();
        }

        //開啟Form之前預先載入程式
        private void mainForm_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(picB_VDpaint.Width, picB_VDpaint.Height);
        }

        //button run 執行VD演算法與畫圖一次到底
        private void btn_run_Click(object sender, EventArgs e)
        {
            Run();
        }

        //button step 執行VD演算法與畫圖到merge之前
        private void btn_step_Click(object sender, EventArgs e)
        {
            Run();//目前只做到三個點，所以不會有merge
        }

        //button next_test 執行下一組測試資料
        private void btn_next_Click(object sender, EventArgs e)
        {
            if (!isReadFile) MessageBox.Show("未讀取測試檔案!");
            else
            {
                initAll();
                Resume();
            }
        }

        //匯出VD points & edges
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (v.lst_points.Count == 0)
            {
                MessageBox.Show("沒有點或是沒有Run資料!!!");
            }
            else
            {
                string txt = string.Empty;
                txt = exportVDtxt();
                saveVDfile(txt);
            }
        }

        //讀取測試file
        private void btn_read_Click(object sender, EventArgs e)
        {
            ParameterizedThreadStart pTrdStart;

            isReadFile = false;
            openFileDialog = new OpenFileDialog(); //瀏覽file
            
            //讀取檔案
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    sr_file = new StreamReader(openFileDialog.FileName, Encoding.Default);
                    
                    isReadFile = true;
                    initAll();
                    MessageBox.Show("資料已輸入，請開始測試!");

                    pTrdStart = new ParameterizedThreadStart(nextStep);
                    _trd = new Thread(pTrdStart);
                    _trd.Start(sr_file);
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        //清除畫布、List
        private void btn_clear_Click(object sender, EventArgs e)
        {
            initAll();
        }

        //畫布上畫點動作
        private void picB_VDpaint_MouseClick(object sender, MouseEventArgs e)
        {
            PointF p = new PointF(e.X, e.Y);
            addPointF(p);
        }

        //做Voronoi Diagram一次做完
        private void Run()
        {
            PointF p_cc = new PointF();     //外心
            PointF p_mid = new PointF();    //中間點
            PointF v_normal = new PointF(); //正規法向量

            //過濾重複的點
            lst_point = lst_point.Distinct().ToList();

            v.clearV();  //voronoi init

            //檢查有多少個點
            if (lst_point.Count == 0) MessageBox.Show("沒有point");
            else if (lst_point.Count == 1)
            {
                clear_picB();

                foreach(PointF p in lst_point)
                {
                    v.lst_points.Add(p);
                    drawPointF(p.X, p.Y, Color.DarkBlue);
                }
            }
            else if (lst_point.Count == 2)
            {
                clear_picB();
                
                lst_point = Calculate.orderPoints(lst_point);  //排序points
                Calculate.calPerpendicularBisector(lst_point[0], lst_point[1], out p_mid, out v_normal); //取得中間點 法向量

                foreach (PointF p in lst_point)
                {
                    v.lst_points.Add(p);
                    drawPointF(p.X, p.Y, Color.DarkBlue);
                }

                v.lst_vor.Add(new Edge(getBorderPoint(p_mid, new PointF(p_mid.X + v_normal.X, p_mid.Y + v_normal.Y)), getBorderPoint(p_mid, new PointF(p_mid.X + v_normal.X * (-1), p_mid.Y + v_normal.Y * (-1)))));
                drawPerpendicularBisector(p_mid, v_normal);
            }
            else if (lst_point.Count == 3)
            {
                clear_picB();

                lst_point = Calculate.orderPoints(lst_point);  //排序points

                foreach (PointF p in lst_point)
                {
                    v.lst_points.Add(p);
                    drawPointF(p.X, p.Y, Color.DarkBlue);
                }

                //三點共線
                if (Calculate.isCollinear(lst_point[0], lst_point[1], lst_point[2]))
                {
                    for (int i = 0; i < lst_point.Count - 1; i++)
                    {
                        Calculate.calPerpendicularBisector(lst_point[i], lst_point[(i + 1) % lst_point.Count], out p_mid, out v_normal);
                        v.lst_vor.Add(new Edge(getBorderPoint(p_mid, new PointF(p_mid.X + v_normal.X, p_mid.Y + v_normal.Y)), getBorderPoint(p_mid, new PointF(p_mid.X + v_normal.X * (-1), p_mid.Y + v_normal.Y * (-1)))));
                        drawPerpendicularBisector(p_mid, v_normal);
                    }
                }
                else
                {
                    //求出外心
                    p_cc = Calculate.calCircumCenter(lst_point[0], lst_point[1], lst_point[2]);

                    lst_point = Calculate.orderVector(lst_point);

                    for (int i = 0; i < lst_point.Count; i++)
                    {
                        Calculate.calPerpendicularBisector(lst_point[i], lst_point[(i+1) % lst_point.Count], out p_mid, out v_normal);
                        v.lst_vor.Add(new Edge(p_cc, getBorderPoint(p_cc, new PointF(p_cc.X + v_normal.X, p_cc.Y + v_normal.Y))));
                        drawLine(p_cc, new PointF(p_cc.X + v_normal.X * 10000, p_cc.Y + v_normal.Y * 10000), Color.Brown);
                    }
                }
            }
        }

        //新增點
        private void addPointF(PointF p)
        {
            if (this.InvokeRequired)  //同執行緒時
            {
                DeladdP deladdP = new DeladdP(addPointF);
                this.Invoke(deladdP, p);
            }
            else
            {
                lsB_nodes.Items.Add("(" + p.X + ", " + p.Y + ")");
                lst_point.Add(p);
                drawPointF(p.X, p.Y, Color.Red);
            }
        }
        
        //在 pictureBox 上畫點
        private void drawPointF(float p_x, float p_y, Color color)
        {
            Graphics g = Graphics.FromImage(bmp);           //在bmp上畫圖
            Brush brush = new SolidBrush(color);            //筆刷顏色
            RectangleF rf = new RectangleF(p_x, p_y, 4, 4);

            g.SmoothingMode = SmoothingMode.AntiAlias;      //消除鋸齒
            g.FillEllipse(brush, rf);
            picB_VDpaint.Image = bmp;
        }

        //在 pictureBox上畫線
        private void drawLine(PointF p1, PointF p2, Color color)
        {
            Graphics g = Graphics.FromImage(bmp);      //在bmp上畫圖
            Pen pen = new Pen(color, 2);               //線顏色

            if (this.InvokeRequired)  //同執行緒時
            {
                DeldrawE deldrawE = new DeldrawE(drawLine);
                this.Invoke(deldrawE, p1, p2, color);
            }
            else
            {
                g.SmoothingMode = SmoothingMode.AntiAlias; //消除鋸齒
                g.DrawLine(pen, p1, p2);
                picB_VDpaint.Image = bmp;
            }
        }
        
        //初始化全部
        private void initAll()
        {
            if (this.InvokeRequired)  //同執行緒時
            {
                DelInit delInit = new DelInit(initAll);
                this.Invoke(delInit);
            }
            else
            {
                lst_point.Clear();
                lst_edge.Clear();
                lsB_nodes.Items.Clear();
                v.lst_points.Clear();
                v.lst_vor.Clear();

                clear_picB();
            }
        }

        //清除pictureBox
        private void clear_picB()
        {
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            picB_VDpaint.Image = bmp;
        }

        //畫出中垂線 2點
        private void drawPerpendicularBisector(PointF p_mid, PointF v_normal)
        {
            drawLine(p_mid, new PointF(p_mid.X + v_normal.X * 10000, p_mid.Y + v_normal.Y * 10000), Color.Brown);
            drawLine(p_mid, new PointF(p_mid.X + v_normal.X * 10000 * (-1), p_mid.Y + v_normal.Y * 10000 * (-1)), Color.Brown);
        }

        //取得線段與邊界的點，用兩點算出斜率與截距得出邊界點
        private PointF getBorderPoint(PointF p1, PointF p2)
        {
            float X_Border = picB_VDpaint.Size.Width;
            float Y_Border = picB_VDpaint.Size.Height;
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;
            float m = dy / dx;          //斜率
            float ic = p1.Y - m * p1.X; //截距 = y - mx

            if (dx == 0 && dy == 0) return new PointF(p1.X, p1.Y); //重疊
            else if (dx == 0) //垂直
            {
                if (dy > 0) return new PointF(p1.X, Y_Border);
                else return new PointF(p1.X, 0);
            }
            else if (dy == 0) //水平
            {
                if (dx > 0) return new PointF(X_Border, p1.Y);
                else return new PointF(0, p1.Y);
            }
            else  //y = ax + b
            {
                if (ic >= 0 && ic <= Y_Border)
                {
                    if ((m < 0 && dy > 0) || (m > 0 && dy < 0)) //m < 0: 往右上斜; m > 0: 往右下斜
                    {
                        return new PointF(0, ic);
                    }
                }
                
                if (X_Border * m + ic >= 0 && X_Border * m + ic <= Y_Border)
                {
                    if ((m < 0 && dy < 0) || (m >0 && dy > 0))
                    {
                        return new PointF(X_Border, X_Border * m + ic);
                    }
                }

                if (ic * (-1) / m > 0 && ic * (-1) / m < X_Border)
                {
                    if ((m < 0 && dy < 0) || (m > 0 && dy < 0))
                    {
                        return new PointF(ic * (-1) / m, 0);
                    }
                }

                if ((Y_Border - ic) / m > 0 && (Y_Border - ic) / m < X_Border)
                {
                    if ((m < 0 && dy > 0) || (m > 0 && dy > 0))
                    {
                        return new PointF((Y_Border - ic) / m, Y_Border);
                    }
                }
                return new PointF();
            }
        }
        
        //匯入VD points 資料處理 (有註解/無註解)
        private String importVDtxt(String txt)
        {
            //找到一行中只有0的結束測試指令，並且將後面字串移除
            if (txt.IndexOf("\r\n0\r\n") != -1)
                txt = txt.Remove(txt.IndexOf("\r\n0\r\n") + 3);

            //清除空白行
            txt = txt.Replace("\r\n\r\n", "\r\n");

            //消除註解comment
            //透過判斷 #與換行符號，將字串移除 
            while (txt.Contains("#"))
            {
                txt = txt.Remove(txt.IndexOf("#"), txt.IndexOf("\r\n", txt.IndexOf("#")) - txt.IndexOf("#") + 2);
            }

            return txt;
        }

        //匯出VD動作
        private void saveVDfile(String text)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(folderBrowserDialog.SelectedPath + "\\VD_data.txt");
                    sw.WriteLine(text);
                    sw.Close(); //Close the file

                    MessageBox.Show("VD_data.txt 已匯出!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
            }
        }

        //匯出資料彙整
        private String exportVDtxt()
        {
            TextBox textVor = new TextBox();
            textVor.Clear();

            v.lst_points = Calculate.orderPoints(v.lst_points);
            v.lst_vor = Calculate.orderEdges(v.lst_vor);

            foreach (PointF p in v.lst_points)
            {
                textVor.Text += "P " + p.X + " " + p.Y + Environment.NewLine;
            }

            foreach (Edge e in v.lst_vor)
            {
                textVor.Text += "E " + e.p1.X + " " + e.p1.Y + " " + e.p2.X + " " + e.p2.Y + Environment.NewLine;
            }

            textVor.Text = textVor.Text + "\b"; //扣去多的換行

            return textVor.Text;
        }

        //執行nextstep
        private void nextStep(Object ob)
        {
            int n;
            String l;
            String[] arr_points;
            String[] arr_edges;
            StreamReader sr = ob as StreamReader;

            lst_point.Clear();
            lst_edge.Clear();

            while ((l = sr.ReadLine()) != null)
            {
                if (l != "")
                {
                    if (l != "0")
                    {
                        if (l[0].ToString() != "#")
                        {
                            if (l[0].ToString() == "P")
                            {
                                arr_points = Regex.Split(l, " ");
                                addPointF(new PointF(Convert.ToSingle(arr_points[1]), Convert.ToSingle(arr_points[2])));
                            }
                            else if (l[0].ToString() == "E")
                            {
                                arr_edges = Regex.Split(l, " ");
                                lst_edge.Add(new Edge(new PointF(Convert.ToSingle(arr_edges[1]), Convert.ToSingle(arr_edges[2])), new PointF(Convert.ToSingle(arr_edges[3]), Convert.ToSingle(arr_edges[4]))));
                                drawLine(new PointF(Convert.ToSingle(arr_edges[1]), Convert.ToSingle(arr_edges[2])), new PointF(Convert.ToSingle(arr_edges[3]), Convert.ToSingle(arr_edges[4])), Color.Brown);
                            }
                            else
                            {
                                n = Convert.ToInt32(l[0].ToString());

                                for (int i = 1; i <= n; i++)
                                {
                                    arr_points = Regex.Split(sr.ReadLine(), " ");
                                    addPointF(new PointF(Convert.ToSingle(arr_points[0]), Convert.ToSingle(arr_points[1])));
                                }

                                Pause();
                                _pauseEvent.WaitOne(Timeout.Infinite);
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                else continue;
            }

            isReadFile = false; //執行完畢，需重新讀檔
            MessageBox.Show("讀入點數為零，檔案測試停止");

            sr.Close();
        }

        //將執行緒暫停
        private void Pause()
        {
            // Set WaitHandle false
            _pauseEvent.Reset();
        }

        //將執行緒繼續執行
        private void Resume()
        {
            // Set WaitHandle true
            _pauseEvent.Set();
        }

        //將執行緒停止
        public void Stop()
        {
            // trigger stop
            _shutdownEvent.Set();
            // if thread suspend, let it resume.
            _pauseEvent.Set();
            _trd.Join();
            _trd = null;
        }
    }
}
