namespace DontForgetTheEggs.Model
{
    public class Grocery
    {
        public int Id { get; set; }
        public Ingredient Ingredient { get; set; }
        public int Quanity { get; set; }
        public bool Checked { get; set; }
    }
}