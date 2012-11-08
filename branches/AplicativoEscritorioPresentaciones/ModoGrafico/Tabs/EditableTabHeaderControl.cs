namespace ModoGrafico.Tabs
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Threading;
    using ModoGrafico.EventArgsClasses;

    /// <summary>
    /// Header Editable TabItem
    /// </summary>
    [TemplatePart(Name = "PART_EditArea", Type = typeof(TextBox))]
    public class EditableTabHeaderControl : ContentControl
    {
        /// <summary>
        /// Dependency property to bind EditMode with XAML Trigger
        /// </summary>
        private static readonly DependencyProperty IsInEditModeProperty = DependencyProperty.Register("IsInEditMode", typeof(bool), typeof(EditableTabHeaderControl));
        private TextBox textBox;
        private string oldText;
        private DispatcherTimer timer;
        private delegate void FocusTextBox();
        public delegate void ClickHandler(object sender, MouseButtonEventArgs e);
        public static event ClickHandler ClickEvento;

        public TabItem SelectedTab { get; set; }
        public int SelectedIndex { get; set; }

        public delegate void HeaderPropertiesClickedHandler(object sender, HeaderPropertiesClickedEventArgs e);
        public static event HeaderPropertiesClickedHandler PropertiesClickEvento;


        public EditableTabHeaderControl()
        {
            //ContextMenu con = new ContextMenu();

            ////falta el icono
            //MenuItem prop = new MenuItem() { Header = "Propiedades", ToolTip = "Propiedades del tab seleccionado" };
            //prop.Click += new RoutedEventHandler(prop_Click);            
            //con.Items.Add(prop);

            //ContextMenu = con;

            //ToolTip = "Click derecho para desplegar el menu";
        }

        void prop_Click(object sender, RoutedEventArgs e)
        {
            HeaderPropertiesClickedEventFire(this, new HeaderPropertiesClickedEventArgs() { NombreContexto = this.Content.ToString() });
        }


        public static void HeaderPropertiesClickedEventFire(object sender, HeaderPropertiesClickedEventArgs e)
        {
            if (PropertiesClickEvento != null)
            {
                PropertiesClickEvento(sender, e);
            }
        }
       

        public static void ClickEventoFire(object sender, MouseButtonEventArgs e)
        {
            if (ClickEvento != null)
            {
                ClickEvento(sender, e);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is in edit mode.
        /// </summary>
        public bool IsInEditMode
        {
            get
            {
                return (bool)this.GetValue(IsInEditModeProperty);
            }
            set
            {
                if (string.IsNullOrEmpty(this.textBox.Text))
                {
                    this.textBox.Text = this.oldText;
                }

                this.oldText = this.textBox.Text;
                this.SetValue(IsInEditModeProperty, value);
            }
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.textBox = this.Template.FindName("PART_EditArea", this) as TextBox;
            if (this.textBox != null)
            {
                this.timer = new DispatcherTimer();
                this.timer.Tick += TimerTick;
                this.timer.Interval = TimeSpan.FromMilliseconds(1);
                this.LostFocus += TextBoxLostFocus;
                this.textBox.KeyDown += TextBoxKeyDown;
                this.MouseDoubleClick += EditableTabHeaderControlMouseDoubleClick;
            }
            this.MouseLeftButtonDown += ClickEventoFire;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            this.timer.Stop();
            this.MoveTextBoxInFocus();
        }

        private void MoveTextBoxInFocus()
        {
            if (this.textBox.CheckAccess())
            {
                if (!string.IsNullOrEmpty(this.textBox.Text))
                {
                    this.textBox.CaretIndex = 0;
                    this.textBox.Focus();
                    this.textBox.SelectAll();
                }
            }
            else
            {
                this.textBox.Dispatcher.BeginInvoke(DispatcherPriority.Render, new FocusTextBox(this.MoveTextBoxInFocus));
            }
        }

        private void TextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.textBox.Text = oldText;
                this.IsInEditMode = false;
            }
            else if (e.Key == Key.Enter)
            {
                this.IsInEditMode = false;
            }
        }

        private void TextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            this.IsInEditMode = false;
        }

        private void EditableTabHeaderControlMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.IsInEditMode = true;
                this.timer.Start();
            }
        }
    }
}