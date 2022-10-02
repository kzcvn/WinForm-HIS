
namespace HIS
{
    partial class DW
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
            this.医生工作台ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.模板维护ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查询统计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.西药处方模板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.检验申请单模板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.门诊日志查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.医生工作台ToolStripMenuItem,
            this.模板维护ToolStripMenuItem,
            this.查询统计ToolStripMenuItem,
            this.系统设置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1282, 28);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 医生工作台ToolStripMenuItem
            // 
            this.医生工作台ToolStripMenuItem.Name = "医生工作台ToolStripMenuItem";
            this.医生工作台ToolStripMenuItem.Size = new System.Drawing.Size(98, 24);
            this.医生工作台ToolStripMenuItem.Text = "医生工作台";
            this.医生工作台ToolStripMenuItem.Click += new System.EventHandler(this.医生工作台ToolStripMenuItem_Click);
            // 
            // 模板维护ToolStripMenuItem
            // 
            this.模板维护ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.西药处方模板ToolStripMenuItem,
            this.检验申请单模板ToolStripMenuItem});
            this.模板维护ToolStripMenuItem.Name = "模板维护ToolStripMenuItem";
            this.模板维护ToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.模板维护ToolStripMenuItem.Text = "模板维护";
            // 
            // 查询统计ToolStripMenuItem
            // 
            this.查询统计ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.门诊日志查询ToolStripMenuItem});
            this.查询统计ToolStripMenuItem.Name = "查询统计ToolStripMenuItem";
            this.查询统计ToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.查询统计ToolStripMenuItem.Text = "查询统计";
            // 
            // 系统设置ToolStripMenuItem
            // 
            this.系统设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.退出ToolStripMenuItem1});
            this.系统设置ToolStripMenuItem.Name = "系统设置ToolStripMenuItem";
            this.系统设置ToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.系统设置ToolStripMenuItem.Text = "系统管理";
            // 
            // 退出ToolStripMenuItem1
            // 
            this.退出ToolStripMenuItem1.Name = "退出ToolStripMenuItem1";
            this.退出ToolStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.退出ToolStripMenuItem1.Text = "退出";
            this.退出ToolStripMenuItem1.Click += new System.EventHandler(this.退出ToolStripMenuItem1_Click);
            // 
            // 西药处方模板ToolStripMenuItem
            // 
            this.西药处方模板ToolStripMenuItem.Name = "西药处方模板ToolStripMenuItem";
            this.西药处方模板ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.西药处方模板ToolStripMenuItem.Text = "西药处方模板";
            // 
            // 检验申请单模板ToolStripMenuItem
            // 
            this.检验申请单模板ToolStripMenuItem.Name = "检验申请单模板ToolStripMenuItem";
            this.检验申请单模板ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.检验申请单模板ToolStripMenuItem.Text = "检验申请单模板";
            // 
            // 门诊日志查询ToolStripMenuItem
            // 
            this.门诊日志查询ToolStripMenuItem.Name = "门诊日志查询ToolStripMenuItem";
            this.门诊日志查询ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.门诊日志查询ToolStripMenuItem.Text = "门诊日志查询";
            // 
            // DW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1282, 673);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DW";
            this.Text = "门诊医生工作站";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 医生工作台ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 模板维护ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查询统计ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 西药处方模板ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 检验申请单模板ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 门诊日志查询ToolStripMenuItem;
    }
}