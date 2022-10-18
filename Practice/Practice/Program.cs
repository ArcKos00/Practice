namespace Practice
{
    public class Program
    {
        private int _sum = 0;

        public delegate void SumHander(int a, int b);
        public event SumHander? Handler;

        public static void Main(string[] args)
        {
            int a = 3;
            int b = 4;
            var thisClass = new Program();
            thisClass.TryCatchMethod(() => { thisClass.SumSubscription(a, b); });
        }

        public void Sum(int x, int y) => _sum += x + y;

        public void SumSubscription(int x, int y)
        {
            Handler += Sum;
            Handler += Sum;
            if (Handler != null)
            {
                foreach (SumHander item in Handler.GetInvocationList())
                {
                    item(x, y);
                }
            }
        }

        public void TryCatchMethod(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch
            {
            }
        }
    }
}