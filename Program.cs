using System;
using Newtonsoft.Json;

namespace HelloWorld;

class Program {  
    static async Task Main(string[] args)
    {
        Console.Clear();
        Console.Write("Digite seu CEP: ");
        int cep = int.Parse(Console.ReadLine());
        ViaCEPResponse response = await makeRequest(cep);
        if(response.ddd != null) {
            Console.WriteLine($"CEP: {response.cep}");
            Console.WriteLine($"Logradouro: {response.logradouro}");
            Console.WriteLine($"Bairro: {response.bairro}");
            Console.WriteLine($"Cidade: {response.localidade}");
            Console.WriteLine($"UF: {response.uf}");
            Console.WriteLine($"complemento: {response.complemento}");
            Console.WriteLine($"ddd: {response.ddd}");
        }
    }

    static async Task<ViaCEPResponse> makeRequest(int cep)
    {
                
        using (HttpClient client = new HttpClient())
        {
            string url = $"https://viacep.com.br/ws/{cep}/json/";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ViaCEPResponse>(json);
            }
            else
            {
                Console.WriteLine($"Erro ao obter o CEP: {response.StatusCode}");
                return new ViaCEPResponse() {ddd = null};
            }
        }
    }
 }




  

        
    

