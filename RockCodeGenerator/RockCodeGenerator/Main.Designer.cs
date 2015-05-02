namespace RockCodeGenerator
{
    partial class Main
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvTables = new System.Windows.Forms.TreeView();
            this.gpButton = new System.Windows.Forms.GroupBox();
            this.btnBLLFactoryObject = new System.Windows.Forms.Button();
            this.btnBllFactoryConfig = new System.Windows.Forms.Button();
            this.btnDaoFactoryObject = new System.Windows.Forms.Button();
            this.btnDaoFactoryConfig = new System.Windows.Forms.Button();
            this.btnWebConfig = new System.Windows.Forms.Button();
            this.btnAjaxConfig = new System.Windows.Forms.Button();
            this.btnDaoConfig = new System.Windows.Forms.Button();
            this.btnBllConfig = new System.Windows.Forms.Button();
            this.btnNormalGenerator = new System.Windows.Forms.Button();
            this.txtContext = new System.Windows.Forms.RichTextBox();
            this.gpConfig = new System.Windows.Forms.GroupBox();
            this.txtNameSpace = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSQL = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gpButton.SuspendLayout();
            this.gpConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvTables);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gpButton);
            this.splitContainer1.Panel2.Controls.Add(this.txtContext);
            this.splitContainer1.Panel2.Controls.Add(this.gpConfig);
            this.splitContainer1.Size = new System.Drawing.Size(1036, 610);
            this.splitContainer1.SplitterDistance = 299;
            this.splitContainer1.TabIndex = 0;
            // 
            // tvTables
            // 
            this.tvTables.CheckBoxes = true;
            this.tvTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvTables.Location = new System.Drawing.Point(0, 0);
            this.tvTables.Name = "tvTables";
            this.tvTables.Size = new System.Drawing.Size(299, 610);
            this.tvTables.TabIndex = 0;
            // 
            // gpButton
            // 
            this.gpButton.Controls.Add(this.btnSQL);
            this.gpButton.Controls.Add(this.btnBLLFactoryObject);
            this.gpButton.Controls.Add(this.btnBllFactoryConfig);
            this.gpButton.Controls.Add(this.btnDaoFactoryObject);
            this.gpButton.Controls.Add(this.btnDaoFactoryConfig);
            this.gpButton.Controls.Add(this.btnWebConfig);
            this.gpButton.Controls.Add(this.btnAjaxConfig);
            this.gpButton.Controls.Add(this.btnDaoConfig);
            this.gpButton.Controls.Add(this.btnBllConfig);
            this.gpButton.Controls.Add(this.btnNormalGenerator);
            this.gpButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gpButton.Location = new System.Drawing.Point(0, 505);
            this.gpButton.Name = "gpButton";
            this.gpButton.Size = new System.Drawing.Size(733, 105);
            this.gpButton.TabIndex = 2;
            this.gpButton.TabStop = false;
            // 
            // btnBLLFactoryObject
            // 
            this.btnBLLFactoryObject.Location = new System.Drawing.Point(510, 47);
            this.btnBLLFactoryObject.Name = "btnBLLFactoryObject";
            this.btnBLLFactoryObject.Size = new System.Drawing.Size(144, 23);
            this.btnBLLFactoryObject.TabIndex = 8;
            this.btnBLLFactoryObject.Text = "生成BLLFactoryObject";
            this.btnBLLFactoryObject.UseVisualStyleBackColor = true;
            this.btnBLLFactoryObject.Click += new System.EventHandler(this.btnBLLFactoryObject_Click);
            // 
            // btnBllFactoryConfig
            // 
            this.btnBllFactoryConfig.Location = new System.Drawing.Point(338, 47);
            this.btnBllFactoryConfig.Name = "btnBllFactoryConfig";
            this.btnBllFactoryConfig.Size = new System.Drawing.Size(144, 23);
            this.btnBllFactoryConfig.TabIndex = 7;
            this.btnBllFactoryConfig.Text = "生成BLLFactoryConfig";
            this.btnBllFactoryConfig.UseVisualStyleBackColor = true;
            this.btnBllFactoryConfig.Click += new System.EventHandler(this.btnBllFactoryConfig_Click);
            // 
            // btnDaoFactoryObject
            // 
            this.btnDaoFactoryObject.Location = new System.Drawing.Point(510, 15);
            this.btnDaoFactoryObject.Name = "btnDaoFactoryObject";
            this.btnDaoFactoryObject.Size = new System.Drawing.Size(144, 23);
            this.btnDaoFactoryObject.TabIndex = 6;
            this.btnDaoFactoryObject.Text = "生成DaoFactoryObject";
            this.btnDaoFactoryObject.UseVisualStyleBackColor = true;
            this.btnDaoFactoryObject.Click += new System.EventHandler(this.btnDaoFactoryObject_Click);
            // 
            // btnDaoFactoryConfig
            // 
            this.btnDaoFactoryConfig.Location = new System.Drawing.Point(338, 15);
            this.btnDaoFactoryConfig.Name = "btnDaoFactoryConfig";
            this.btnDaoFactoryConfig.Size = new System.Drawing.Size(144, 23);
            this.btnDaoFactoryConfig.TabIndex = 5;
            this.btnDaoFactoryConfig.Text = "生成DaoFactoryConfig";
            this.btnDaoFactoryConfig.UseVisualStyleBackColor = true;
            this.btnDaoFactoryConfig.Click += new System.EventHandler(this.btnDaoFactoryConfig_Click);
            // 
            // btnWebConfig
            // 
            this.btnWebConfig.Location = new System.Drawing.Point(338, 76);
            this.btnWebConfig.Name = "btnWebConfig";
            this.btnWebConfig.Size = new System.Drawing.Size(88, 23);
            this.btnWebConfig.TabIndex = 4;
            this.btnWebConfig.Text = "生成Web配置";
            this.btnWebConfig.UseVisualStyleBackColor = true;
            this.btnWebConfig.Click += new System.EventHandler(this.btnWebConfig_Click);
            // 
            // btnAjaxConfig
            // 
            this.btnAjaxConfig.Location = new System.Drawing.Point(217, 76);
            this.btnAjaxConfig.Name = "btnAjaxConfig";
            this.btnAjaxConfig.Size = new System.Drawing.Size(88, 23);
            this.btnAjaxConfig.TabIndex = 3;
            this.btnAjaxConfig.Text = "生成Ajax配置";
            this.btnAjaxConfig.UseVisualStyleBackColor = true;
            this.btnAjaxConfig.Click += new System.EventHandler(this.btnAjaxConfig_Click);
            // 
            // btnDaoConfig
            // 
            this.btnDaoConfig.Location = new System.Drawing.Point(217, 15);
            this.btnDaoConfig.Name = "btnDaoConfig";
            this.btnDaoConfig.Size = new System.Drawing.Size(83, 23);
            this.btnDaoConfig.TabIndex = 2;
            this.btnDaoConfig.Text = "生成Dao配置";
            this.btnDaoConfig.UseVisualStyleBackColor = true;
            this.btnDaoConfig.Click += new System.EventHandler(this.btnDaoConfig_Click);
            // 
            // btnBllConfig
            // 
            this.btnBllConfig.Location = new System.Drawing.Point(217, 44);
            this.btnBllConfig.Name = "btnBllConfig";
            this.btnBllConfig.Size = new System.Drawing.Size(86, 23);
            this.btnBllConfig.TabIndex = 1;
            this.btnBllConfig.Text = "生成BLL配置";
            this.btnBllConfig.UseVisualStyleBackColor = true;
            this.btnBllConfig.Click += new System.EventHandler(this.btnBllConfig_Click);
            // 
            // btnNormalGenerator
            // 
            this.btnNormalGenerator.Location = new System.Drawing.Point(21, 15);
            this.btnNormalGenerator.Name = "btnNormalGenerator";
            this.btnNormalGenerator.Size = new System.Drawing.Size(75, 23);
            this.btnNormalGenerator.TabIndex = 0;
            this.btnNormalGenerator.Text = "生成";
            this.btnNormalGenerator.UseVisualStyleBackColor = true;
            this.btnNormalGenerator.Click += new System.EventHandler(this.btnNormalGenerator_Click);
            // 
            // txtContext
            // 
            this.txtContext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContext.Location = new System.Drawing.Point(0, 80);
            this.txtContext.Name = "txtContext";
            this.txtContext.Size = new System.Drawing.Size(733, 530);
            this.txtContext.TabIndex = 1;
            this.txtContext.Text = "";
            // 
            // gpConfig
            // 
            this.gpConfig.Controls.Add(this.txtNameSpace);
            this.gpConfig.Controls.Add(this.label1);
            this.gpConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpConfig.Location = new System.Drawing.Point(0, 0);
            this.gpConfig.Name = "gpConfig";
            this.gpConfig.Size = new System.Drawing.Size(733, 80);
            this.gpConfig.TabIndex = 0;
            this.gpConfig.TabStop = false;
            this.gpConfig.Text = "配置";
            // 
            // txtNameSpace
            // 
            this.txtNameSpace.Location = new System.Drawing.Point(113, 24);
            this.txtNameSpace.Name = "txtNameSpace";
            this.txtNameSpace.Size = new System.Drawing.Size(429, 21);
            this.txtNameSpace.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "二级生成命名空间：";
            // 
            // btnSQL
            // 
            this.btnSQL.Location = new System.Drawing.Point(510, 76);
            this.btnSQL.Name = "btnSQL";
            this.btnSQL.Size = new System.Drawing.Size(99, 23);
            this.btnSQL.TabIndex = 9;
            this.btnSQL.Text = "生成自定义SQL";
            this.btnSQL.UseVisualStyleBackColor = true;
            this.btnSQL.Click += new System.EventHandler(this.btnSQL_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 610);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gpButton.ResumeLayout(false);
            this.gpConfig.ResumeLayout(false);
            this.gpConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox gpButton;
        private System.Windows.Forms.RichTextBox txtContext;
        private System.Windows.Forms.GroupBox gpConfig;
        private System.Windows.Forms.Button btnNormalGenerator;
        private System.Windows.Forms.TextBox txtNameSpace;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnWebConfig;
        private System.Windows.Forms.Button btnAjaxConfig;
        private System.Windows.Forms.Button btnDaoConfig;
        private System.Windows.Forms.Button btnBllConfig;
        private System.Windows.Forms.Button btnDaoFactoryConfig;
        private System.Windows.Forms.Button btnDaoFactoryObject;
        private System.Windows.Forms.Button btnBllFactoryConfig;
        private System.Windows.Forms.Button btnBLLFactoryObject;
        private System.Windows.Forms.TreeView tvTables;
        private System.Windows.Forms.Button btnSQL;
    }
}