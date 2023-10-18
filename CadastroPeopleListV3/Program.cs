using Controller;
using System;

namespace CadastroPeopleListV3
{
    internal class Program
    {
        private static ICadastraPessoa iCadastraPessoa;

        private static void AddPerson() => iCadastraPessoa.AddPerson();

        private static void ShowPeople() => iCadastraPessoa.ShowPeople();

        private static void SearchPerson() => iCadastraPessoa.SearchPerson();

        private static void RemovePerson() => iCadastraPessoa.RemovePerson();

        private static void EditPerson() => iCadastraPessoa.EditPerson();

        private static ICadastraPessoa getInstanceCadastraPessoa()
        {
            return new CadastraPessoa();
        }

        static void Main(string[] args)
        {
            iCadastraPessoa = getInstanceCadastraPessoa();

            string option = "";
            while (option != "0")
            {
                Console.WriteLine("Escolha uma opção: ");
                Console.WriteLine("[ 1 ] - Adicionar Pessoa");
                Console.WriteLine("[ 2 ] - Exibir Pessoas");
                Console.WriteLine("[ 3 ] - Buscar Pessoas");
                Console.WriteLine("[ 4 ] - Remover Pessoa");
                Console.WriteLine("[ 5 ] - Editar Pessoa");
                Console.WriteLine("[ 0 ] - Sair");
                option = Convert.ToString(Console.ReadLine());

                switch (option)
                {
                    case "1":
                        AddPerson();
                        break;
                    case "2":
                        ShowPeople();
                        break;
                    case "3":
                        SearchPerson();
                        break;
                    case "4":
                        RemovePerson();
                        break;
                    case "5":
                        EditPerson();
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Opção inválida");
                        break;
                }
            }           
        }
    }
}