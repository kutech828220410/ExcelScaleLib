
namespace TestForm
{
    partial class Form1
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
            this.button_初始化 = new System.Windows.Forms.Button();
            this.button_讀取_g = new System.Windows.Forms.Button();
            this.button_扣重 = new System.Windows.Forms.Button();
            this.button_讀取_錢 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_初始化
            // 
            this.button_初始化.Location = new System.Drawing.Point(12, 39);
            this.button_初始化.Name = "button_初始化";
            this.button_初始化.Size = new System.Drawing.Size(81, 42);
            this.button_初始化.TabIndex = 0;
            this.button_初始化.Text = "初始化";
            this.button_初始化.UseVisualStyleBackColor = true;
            // 
            // button_讀取_g
            // 
            this.button_讀取_g.Location = new System.Drawing.Point(99, 39);
            this.button_讀取_g.Name = "button_讀取_g";
            this.button_讀取_g.Size = new System.Drawing.Size(81, 42);
            this.button_讀取_g.TabIndex = 1;
            this.button_讀取_g.Text = "讀取(g)";
            this.button_讀取_g.UseVisualStyleBackColor = true;
            // 
            // button_扣重
            // 
            this.button_扣重.Location = new System.Drawing.Point(99, 87);
            this.button_扣重.Name = "button_扣重";
            this.button_扣重.Size = new System.Drawing.Size(81, 42);
            this.button_扣重.TabIndex = 2;
            this.button_扣重.Text = "扣重";
            this.button_扣重.UseVisualStyleBackColor = true;
            // 
            // button_讀取_錢
            // 
            this.button_讀取_錢.Location = new System.Drawing.Point(186, 39);
            this.button_讀取_錢.Name = "button_讀取_錢";
            this.button_讀取_錢.Size = new System.Drawing.Size(81, 42);
            this.button_讀取_錢.TabIndex = 3;
            this.button_讀取_錢.Text = "讀取(錢)";
            this.button_讀取_錢.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 532);
            this.Controls.Add(this.button_讀取_錢);
            this.Controls.Add(this.button_扣重);
            this.Controls.Add(this.button_讀取_g);
            this.Controls.Add(this.button_初始化);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_初始化;
        private System.Windows.Forms.Button button_讀取_g;
        private System.Windows.Forms.Button button_扣重;
        private System.Windows.Forms.Button button_讀取_錢;
    }
}

