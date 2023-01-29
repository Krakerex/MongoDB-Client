namespace Projekt
{
    partial class Utwórz
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Utwórz));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.addColumn = new System.Windows.Forms.Button();
            this.addRow = new System.Windows.Forms.Button();
            this.createRecords = new System.Windows.Forms.Button();
            this.deleteColumn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.deleteRow = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(609, 387);
            this.dataGridView1.TabIndex = 0;
            // 
            // addColumn
            // 
            this.addColumn.Location = new System.Drawing.Point(627, 12);
            this.addColumn.Name = "addColumn";
            this.addColumn.Size = new System.Drawing.Size(75, 23);
            this.addColumn.TabIndex = 1;
            this.addColumn.Text = "Dodaj kolumne";
            this.addColumn.UseVisualStyleBackColor = true;
            this.addColumn.Click += new System.EventHandler(this.addColumn_Click);
            // 
            // addRow
            // 
            this.addRow.Location = new System.Drawing.Point(12, 405);
            this.addRow.Name = "addRow";
            this.addRow.Size = new System.Drawing.Size(75, 23);
            this.addRow.TabIndex = 2;
            this.addRow.Text = "Dodaj";
            this.addRow.UseVisualStyleBackColor = true;
            this.addRow.Click += new System.EventHandler(this.addRow_Click);
            // 
            // createRecords
            // 
            this.createRecords.Location = new System.Drawing.Point(272, 406);
            this.createRecords.Name = "createRecords";
            this.createRecords.Size = new System.Drawing.Size(190, 32);
            this.createRecords.TabIndex = 3;
            this.createRecords.Text = "Dodaj dane do kolekcji";
            this.createRecords.UseVisualStyleBackColor = true;
            this.createRecords.Click += new System.EventHandler(this.createRecords_Click);
            // 
            // deleteColumn
            // 
            this.deleteColumn.Location = new System.Drawing.Point(708, 12);
            this.deleteColumn.Name = "deleteColumn";
            this.deleteColumn.Size = new System.Drawing.Size(75, 23);
            this.deleteColumn.TabIndex = 4;
            this.deleteColumn.Text = "Usuń";
            this.deleteColumn.UseVisualStyleBackColor = true;
            this.deleteColumn.Click += new System.EventHandler(this.deleteColumn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(627, 68);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(156, 20);
            this.textBox1.TabIndex = 5;
            // 
            // deleteRow
            // 
            this.deleteRow.Location = new System.Drawing.Point(93, 405);
            this.deleteRow.Name = "deleteRow";
            this.deleteRow.Size = new System.Drawing.Size(75, 23);
            this.deleteRow.TabIndex = 6;
            this.deleteRow.Text = "Usuń";
            this.deleteRow.UseVisualStyleBackColor = true;
            this.deleteRow.Click += new System.EventHandler(this.deleteRow_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(667, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Nazwa kolumny";
            // 
            // Utwórz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.deleteRow);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.deleteColumn);
            this.Controls.Add(this.createRecords);
            this.Controls.Add(this.addRow);
            this.Controls.Add(this.addColumn);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Utwórz";
            this.Text = "Utwórz";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button addColumn;
        private System.Windows.Forms.Button addRow;
        private System.Windows.Forms.Button createRecords;
        private System.Windows.Forms.Button deleteColumn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button deleteRow;
        private System.Windows.Forms.Label label1;
    }
}