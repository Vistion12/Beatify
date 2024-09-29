

using var client = new HttpClient();

var responsemesage = await client.GetAsync("https://localhost:7037/Groups");


var response= await responsemesage.Content.ReadAsStringAsync();


Console.ReadKey();