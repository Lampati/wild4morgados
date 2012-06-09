namespace DiagramDesigner.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        private string productName;

        public string ProductName
        {
            get
            {
                return this.productName;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.productName = value;
                    NotifyPropertyChanged("ProductName");
                }
            }
        }
    }
}
