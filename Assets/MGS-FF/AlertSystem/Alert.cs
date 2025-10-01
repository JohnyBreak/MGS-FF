namespace AlertSystem
{
    public class Alert
    {
        private readonly AlertContext _context;
        private readonly AlertVision _vision;
        
        public Alert(AlertView view)
        {
            _vision = new();
            _context = new AlertContext(view, _vision);
        }

        public void Increase()
        {
            _vision.Increase();
        }
        
        public void Decrease()
        {
            _vision.Decrease();
        }
    }    
}

