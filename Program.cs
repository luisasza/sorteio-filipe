using System;

// determina caminho que vai ser usado (caminho da pasta Models que contém a classe Sorteio)
using sorteio_filipe.Models;




// main

    class Program
    {
        static void Main()
        {
            // Instancia a classe Sorteio
            Sorteio sorteio = new Sorteio();

            // Chama o método RealizarSorteio()
            sorteio.RealizarSorteio();
        }
    }
