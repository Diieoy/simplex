using Caliburn.Micro;

namespace WpfApp.Models
{
    public class SeatModel : PropertyChangedBase
    {
        public int Id { get; set; }
        public int AreaId { get; set; }
        public int Row { get; set; }
        private int number;
        public int Number
        {
            get { return number; }
            set
            {
                number = value;
                NotifyOfPropertyChange();
            }
        }
    }
}
