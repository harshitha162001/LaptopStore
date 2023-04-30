namespace activity1.Models
{
    public class Confirmation
    {
        public int ConfirmationId
        {
            get; set;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Payment { get; set; }
        public int CardNumber { get; set; }
    }
}
