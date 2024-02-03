using System;

// modificadores de acesso em C#: 

// public: o tipo ou membro pode ser acessado por qualquer outro código no mesmo assembly ou em outro assembly 
// que faz referência a ele. O nível de acessibilidade de membros públicos de um tipo é controlado pelo nível de 
// acessibilidade do próprio tipo.

// private: o tipo ou membro pode ser acessado somente pelo código na mesma class ou struct.

// protected: o tipo ou membro pode ser acessado somente pelo código na mesma class ou em uma class derivada dessa class.


// classe pública que vai guardar todos os métodos necessários para realizar o sorteio e verificar as respostas

namespace sorteio_filipe.Models
{
    public class Sorteio
    {

         // declara as variáveis com valores iniciais, para caso não seja digitado nada no console, hajam valores default pra rodar o programa:
        private int sorteio;
        private int tentativas = 1;
        private int min = 1;
        private int max = 100;


        // método público (que vai ser chamado pelo program (main)) para realizar o sorteio. void significa que o método não retorna nada
        public void RealizarSorteio()
        {
            // try e catch sao formas de encapsular o bloco de código para tratar exceções (evitar que o programa termine por conta de comportamento não previsto)
            try
            {
                // métodos que vão ser chamados por RealizarSorteio() para dar início ao sorteio
                ObterParametros();
                IniciarSorteio();

                // loop que vai cuidar das tentativas de acerto. enquanto tentativas > 0, obter palpite e verificar palpite. então, exibir resultado
                while (tentativas > 0)
                {
                    // outro bloco try catch pra pegar erros
                    try
                    {
                        int numero = ObterPalpiteUsuario();
                        VerificarPalpite(numero);
                    }
                    // catch que determina o que fazer caso a parte do try der alguma exceção
                    catch (FormatException)
                    {
                        Console.WriteLine("Por favor, digite um número válido.");
                    }
                }

                ExibirResultado();
            }
            // catch que determina o que fazer caso a parte do try der alguma exceção
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
            }
        }

        // método privado (somente acessível pela própria class Sorteio. Não pode ser alterado fora da Classe)
        // e void porque não retorna nada. Obtém valores que vão determinar os parâmetros do sorteio
        private void ObterParametros()
        {
            // escreve no console que o sorteio foi iniciado
            Console.WriteLine("Sorteio inicializado. Defina o menor e maior número do intervalo a ser sorteado, nesta ordem.");

            // Obtém do console os números que vão ser guardados nas variáveis min, max e tentativas, chamando o método ObterNumeroDigitado()
            min = ObterNumeroDigitado("Menor número possível definido como ", min);
            max = ObterNumeroDigitado("Maior número possível definido como ", max);
            tentativas = ObterNumeroDigitado("Número de tentativas a serem aceitas pelo programa ", tentativas);
        }

        // método privado (somente acessível pela própria class Sorteio. Não pode ser alterado fora da Classe)
        // e int (não void) porque retorna número inteiro. Obtém número (é chamado em ObterParametros() para determinar min, max, e tentativas)
        // recebe 2 parâmetros, o prompt a ser exibito no console e o número que vai ser guardado
        private int ObterNumeroDigitado(string prompt, int defaultValue)
        {
            // mostra no console o prompt que foi recebido como parâmetro
            Console.Write(prompt);

            // guarda na variável userInput o número a ser digitado no console
            string userInput = Console.ReadLine();

            // expressão condicional (if) que verifica se houve valor recebido. se sim, é convertido pra int
            // se não houve valor recebido, o programa continuará com os valores determinados lá no início
            // da classe Sorteio quando as variáveis foram declaradas com valores iniciais
            return string.IsNullOrWhiteSpace(userInput) ? defaultValue : Convert.ToInt32(userInput);
        }

        // método privado (accessível na classe) e void (retorna nada) que sorteia um número a ser guessed
        private void IniciarSorteio()
        {
            Console.WriteLine($"Você terá {tentativas} tentativas para advinhar um número entre {min} e {max}. Digite um número:");
            sorteio = new Random().Next(min, max);
        }

        // método privado (acessível na classe) e int (retorna inteiro) que vai receber palpites do console
        private int ObterPalpiteUsuario()
        {
            Console.Write("Digite um número: ");
            return Convert.ToInt32(Console.ReadLine());
        }

        // método privado e void que vai comparar palpites recebidos com o número sorteado
        private void VerificarPalpite(int numero)
        {
            // switch case é uma verificação condicional. switch (condição), case: fazer

            // compara número verificado ao número do sorteio
            switch (Math.Sign(numero.CompareTo(sorteio)))
            {
                // se número verificado for igual ao número recebido
                case 0:
                    Console.WriteLine("Parabéns, você acertou!");
                    tentativas = 0;
                    break;
                // se o número verificado for maior que o número sorteado 
                case 1:
                    Console.WriteLine("O número sorteado é menor que o digitado");
                    tentativas--;
                    break;

                // se o número verificado for menor que o número sorteado
                case -1:
                    Console.WriteLine("O número sorteado é maior que o digitado");
                    tentativas--;
                    break;
            }
        }

        // método que vai exibir mensagem quando terminarem as tentavidas.
        private void ExibirResultado()
        {
            Console.WriteLine(tentativas == 0
                ? $"Fim das tentativas. O número sorteado era: {sorteio}"
                : "Algo deu errado. O programa não deveria chegar aqui.");
        }
    }
}