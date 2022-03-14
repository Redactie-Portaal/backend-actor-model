namespace WebApplication3.Controllers
{

    public class SomeObject
    {
        public SomeObject(string hello, string test, int a, List<string> vs)
        {
            Hello = hello;
            Test = test;
            this.a = a;
            this.vs = vs;
        }

        public string Hello { get; set; }
        public string Test { get; set; }
        public int a { get; set; }
        public List<string> vs { get; set; }
    }

}
