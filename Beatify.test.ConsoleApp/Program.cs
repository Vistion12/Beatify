Console.WriteLine("Hello, World!");

using (var client =new HttpClient())
{
    using var result = await client.GetAsync("https://localhost:7037/Groups");
    string responseBody=await result.Content.ReadAsStringAsync();
    Console.WriteLine(result.RequestMessage);
}
Console.WriteLine("the over");
