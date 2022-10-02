
namespace HIS
{
    partial class Regis
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.挂号管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.当日挂号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.挂号重打ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.诊疗卡管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查询统计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.挂号报表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.挂号管理ToolStripMenuItem,
            this.诊疗卡管理ToolStripMenuItem,
            this.查询统计ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1280, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 挂号管理ToolStripMenuItem
            // 
            this.挂号管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.当日挂号ToolStripMenuItem,
            this.挂号重打ToolStripMenuItem});
            this.挂号管理ToolStripMenuItem.Name = "挂号管理ToolStripMenuItem";
            this.挂号管理ToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.挂号管理ToolStripMenuItem.Text = "挂号管理";
            // 
            // 当日挂号ToolStripMenuItem
            // 
            this.当日挂号ToolStripMenuItem.Name = "当日挂号ToolStripMenuItem";
            this.当日挂号ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.当日挂号ToolStripMenuItem.Text = "门急诊挂号";
            this.当日挂号ToolStripMenuItem.Click += new System.EventHandler(this.当日挂号ToolStripMenuItem_Click);
            // 
            // 挂号重打ToolStripMenuItem
            // 
            this.挂号重打ToolStripMenuItem.Name = "挂号重打ToolStripMenuItem";
            this.挂号重打ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.挂号重打ToolStripMenuItem.Text = "退号";
            // 
            // 诊疗卡管理ToolStripMenuItem
            // 
            this.诊疗卡管理ToolStripMenuItem.Name = "诊疗卡管理ToolStripMenuItem";
            this.诊疗卡管理ToolStripMenuItem.Size = new System.Drawing.Size(98, 24);
            this.诊疗卡管理ToolStripMenuItem.Text = "诊疗卡管理";
            this.诊疗卡管理ToolStripMenuItem.Click += new System.EventHandler(this.诊疗卡管理ToolStripMenuItem_Click);
            // 
            // 查询统计ToolStripMenuItem
            // 
            this.查询统计ToolStripMenuItem.Name = "查询统计ToolStripMenuItem";
            this.查询统计ToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.查询统计ToolStripMenuItem.Text = "查询统计";
            this.查询统计ToolStripMenuItem.Click += new System.EventHandler(this.挂号重打ToolStripMenuItem1_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.退出ToolStripMenuItem1});
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.退出ToolStripMenuItem.Text = "系统管理";
            // 
            // 退出ToolStripMenuItem1
            // 
            this.退出ToolStripMenuItem1.Name = "退出ToolStripMenuItem1";
            this.退出ToolStripMenuItem1.Size = new System.Drawing.Size(122, 26);
            this.退出ToolStripMenuItem1.Text = "退出";
            this.退出ToolStripMenuItem1.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 挂号报表ToolStripMenuItem
            // 
            this.挂号报表ToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.挂号报表ToolStripMenuItem.Name = "挂号报表ToolStripMenuItem";
            this.挂号报表ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.挂号报表ToolStripMenuItem.Text = "挂号报表";
            // 
            // Regis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 678);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Regis";
            this.Text = "门急诊挂号系统";
            this.Load += new System.EventHandler(this.Regis_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 挂号管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 当日挂号ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 挂号重打ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 诊疗卡管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 挂号报表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查询统计ToolStripMenuItem;
    }
}