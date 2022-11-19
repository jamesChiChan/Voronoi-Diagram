namespace VoronoiDiagram
{
    partial class mainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panel_VD = new System.Windows.Forms.Panel();
            this.btn_clear = new DevExpress.XtraEditors.SimpleButton();
            this.btn_read = new DevExpress.XtraEditors.SimpleButton();
            this.btn_save = new DevExpress.XtraEditors.SimpleButton();
            this.btn_next = new DevExpress.XtraEditors.SimpleButton();
            this.btn_step = new DevExpress.XtraEditors.SimpleButton();
            this.btn_run = new DevExpress.XtraEditors.SimpleButton();
            this.lsB_nodes = new System.Windows.Forms.ListBox();
            this.picB_VDpaint = new System.Windows.Forms.PictureBox();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            this.panel_VD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picB_VDpaint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.panel_VD);
            this.layoutControl1.Controls.Add(this.lsB_nodes);
            this.layoutControl1.Controls.Add(this.picB_VDpaint);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(934, 631);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panel_VD
            // 
            this.panel_VD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_VD.Controls.Add(this.btn_clear);
            this.panel_VD.Controls.Add(this.btn_read);
            this.panel_VD.Controls.Add(this.btn_save);
            this.panel_VD.Controls.Add(this.btn_next);
            this.panel_VD.Controls.Add(this.btn_step);
            this.panel_VD.Controls.Add(this.btn_run);
            this.panel_VD.Location = new System.Drawing.Point(729, 12);
            this.panel_VD.Name = "panel_VD";
            this.panel_VD.Size = new System.Drawing.Size(193, 607);
            this.panel_VD.TabIndex = 6;
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(33, 551);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(125, 34);
            this.btn_clear.TabIndex = 5;
            this.btn_clear.Text = "Clear";
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_read
            // 
            this.btn_read.Location = new System.Drawing.Point(33, 379);
            this.btn_read.Name = "btn_read";
            this.btn_read.Size = new System.Drawing.Size(125, 34);
            this.btn_read.TabIndex = 4;
            this.btn_read.Text = "Read VD";
            this.btn_read.Click += new System.EventHandler(this.btn_read_Click);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(33, 339);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(125, 34);
            this.btn_save.TabIndex = 3;
            this.btn_save.Text = "Save VD";
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_next
            // 
            this.btn_next.Location = new System.Drawing.Point(33, 171);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(125, 34);
            this.btn_next.TabIndex = 2;
            this.btn_next.Text = "Next test";
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // btn_step
            // 
            this.btn_step.Location = new System.Drawing.Point(33, 43);
            this.btn_step.Name = "btn_step";
            this.btn_step.Size = new System.Drawing.Size(125, 34);
            this.btn_step.TabIndex = 1;
            this.btn_step.Text = "Step by step";
            this.btn_step.Click += new System.EventHandler(this.btn_step_Click);
            // 
            // btn_run
            // 
            this.btn_run.Location = new System.Drawing.Point(33, 3);
            this.btn_run.Name = "btn_run";
            this.btn_run.Size = new System.Drawing.Size(125, 34);
            this.btn_run.TabIndex = 0;
            this.btn_run.Text = "Run";
            this.btn_run.Click += new System.EventHandler(this.btn_run_Click);
            // 
            // lsB_nodes
            // 
            this.lsB_nodes.BackColor = System.Drawing.SystemColors.Control;
            this.lsB_nodes.FormattingEnabled = true;
            this.lsB_nodes.ItemHeight = 12;
            this.lsB_nodes.Location = new System.Drawing.Point(12, 12);
            this.lsB_nodes.Name = "lsB_nodes";
            this.lsB_nodes.Size = new System.Drawing.Size(97, 604);
            this.lsB_nodes.TabIndex = 5;
            // 
            // picB_VDpaint
            // 
            this.picB_VDpaint.BackColor = System.Drawing.Color.White;
            this.picB_VDpaint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picB_VDpaint.Location = new System.Drawing.Point(113, 12);
            this.picB_VDpaint.Name = "picB_VDpaint";
            this.picB_VDpaint.Size = new System.Drawing.Size(612, 607);
            this.picB_VDpaint.TabIndex = 4;
            this.picB_VDpaint.TabStop = false;
            this.picB_VDpaint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picB_VDpaint_MouseClick);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(934, 631);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.picB_VDpaint;
            this.layoutControlItem1.Location = new System.Drawing.Point(101, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(616, 611);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.lsB_nodes;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(101, 611);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.panel_VD;
            this.layoutControlItem3.Location = new System.Drawing.Point(717, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(197, 611);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 631);
            this.Controls.Add(this.layoutControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "mainForm";
            this.Text = "Voronoi Diagram";
            this.Load += new System.EventHandler(this.mainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            this.panel_VD.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picB_VDpaint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private System.Windows.Forms.Panel panel_VD;
        private System.Windows.Forms.ListBox lsB_nodes;
        private System.Windows.Forms.PictureBox picB_VDpaint;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SimpleButton btn_clear;
        private DevExpress.XtraEditors.SimpleButton btn_read;
        private DevExpress.XtraEditors.SimpleButton btn_save;
        private DevExpress.XtraEditors.SimpleButton btn_next;
        private DevExpress.XtraEditors.SimpleButton btn_step;
        private DevExpress.XtraEditors.SimpleButton btn_run;
    }
}

