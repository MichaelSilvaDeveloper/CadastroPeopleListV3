namespace Model
{
    public class Person
    {
        public Person(string name, int age, int telephone, string cpf)
        {
            Name = name;
            Age = age;
            Telephone = telephone;
            Cpf = cpf;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public int Telephone { get; set; }

        public string Cpf { get; set; }
    }
}
