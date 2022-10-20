using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

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
            var ints = new List<IContact>();
            thisClass.TryCatchMethod(() => { thisClass.ActionWithList(ints); });
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

        public void ActionWithList(List<IContact> list)
        {
            var commonList = new List<List<IContact>>();
            int count = 5;

            list = RandomContactAdd(list, count);
            commonList.Add(list);

            var list1 = list.OrderBy(o => o.FullName).ToList();
            commonList.Add(list1);

            var list2 = list.Where(w => w.FullName.StartsWith("р")).ToList();
            commonList.Add(list2);

            var list3 = list.Select(s => s.FullName).ToList();
            var list4 = list.FirstOrDefault();
            var list5 = list.All(a => a.FullName.StartsWith("р"));
            var list6 = list.ElementAtOrDefault(95);
        }

        public List<IContact> RandomContactAdd(List<IContact> list, int count)
        {
            int wordLength = 5;
            for (int i = 0; i < count; i++)
            {
                list.Add(new Contact() { Name = RandomName(wordLength), LastName = RandomName(wordLength) });
            }

            return list;
        }

        private string RandomName(int count)
        {
            char minRange = 'а';
            char maxRange = 'я';
            Random rand = new Random();
            var sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.Append((char)rand.Next(minRange, maxRange));
            }

            return sb.ToString();
        }
    }
}