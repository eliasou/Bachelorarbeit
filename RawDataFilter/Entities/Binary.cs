namespace RawDataFilter.Entities
{
    public class Binary
    {
        public int value;

        public Binary(int value)
        {
            this.value = value;
        }

        public static implicit operator Binary(string b)
        {
            return new Binary(Convert.ToInt32(b, 2));
        }

        public static explicit operator int(Binary b)
        {
            return b.value;
        }
        
    }
}