namespace WebApi.Domain
{
    public class Pizza
    {
        public Pizza(PizzaType type)
        {
            PizzaType = type;
        }

        public PizzaType PizzaType { get; set; }
    }
}