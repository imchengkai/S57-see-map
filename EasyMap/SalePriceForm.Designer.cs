using EasyMap.Controls;
namespace EasyMap
{
    partial class SalePriceForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalePriceForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new EasyMap.Controls.MyDataGridView();
            this.SaleDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SalePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updatedate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOk = new EasyMap.Controls.MyButton();
            this.btnClose = new EasyMap.Controls.MyButton();
            this.paste = new EasyMap.Controls.MyButton();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "历史地价记录：";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoRowHeight = false;
            this.dataGridView1.BackColor1 = System.Drawing.Color.White;
            this.dataGridView1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SaleDate,
            this.SalePrice,
            this.Price,
            this.updatedate});
            this.dataGridView1.ColumnSpanSettings = ((System.Collections.Generic.List<string>)(resources.GetObject("dataGridView1.ColumnSpanSettings")));
            this.dataGridView1.Location = new System.Drawing.Point(10, 22);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeaderTexts = ((System.Collections.Generic.List<string>)(resources.GetObject("dataGridView1.RowHeaderTexts")));
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.ShowLineNo = true;
            this.dataGridView1.Size = new System.Drawing.Size(340, 179);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // SaleDate
            // 
            this.SaleDate.DataPropertyName = "SaleDate";
            this.SaleDate.HeaderText = "监测日期";
            this.SaleDate.Name = "SaleDate";
            this.SaleDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SalePrice
            // 
            this.SalePrice.DataPropertyName = "SalePrice";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SalePrice.DefaultCellStyle = dataGridViewCellStyle2;
            this.SalePrice.HeaderText = "售价";
            this.SalePrice.Name = "SalePrice";
            // 
            // Price
            // 
            this.Price.DataPropertyName = "Price";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Price.DefaultCellStyle = dataGridViewCellStyle3;
            this.Price.HeaderText = "单价";
            this.Price.Name = "Price";
            // 
            // updatedate
            // 
            this.updatedate.DataPropertyName = "UpdateDate";
            this.updatedate.HeaderText = "updatedate";
            this.updatedate.Name = "updatedate";
            this.updatedate.Visible = false;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(196, 207);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 11;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(277, 207);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // paste
            // 
            this.paste.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.paste.Location = new System.Drawing.Point(115, 207);
            this.paste.Name = "paste";
            this.paste.Size = new System.Drawing.Size(75, 23);
            this.paste.TabIndex = 13;
            this.paste.Text = "粘贴(&O)";
            this.paste.UseVisualStyleBackColor = true;
            this.paste.Click += new System.EventHandler(this.paste_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.paste);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(363, 239);
            this.panel1.TabIndex = 14;
            // 
            // SalePriceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(369, 266);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(369, 266);
            this.Name = "SalePriceForm";
            this.ShowInTaskbar = false;
            this.Text = "历史地价监测记录";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SalePriceForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private MyDataGridView dataGridView1;
        private MyButton btnOk;
        private MyButton btnClose;
        private MyButton paste;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn updatedate;
        private System.Windows.Forms.Panel panel1;
    }
}