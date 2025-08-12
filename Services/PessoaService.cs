using MongoDB.Driver;
using Backend.Models;
using Microsoft.Extensions.Options;

namespace Backend.Services
{
  public class PessoaService
  {
    private readonly IMongoDatabase<Pessoa> _pessoasCollection; //Cria uma interface que representa a coleção Pessoas no mongodb, <Pessoa> indica que os documentos dessa coleção serão mapeados para a classe Pessoa.

    public PessoaService(IOptions<MongoDbSettings> mongoDbSettings) //Construtor que injeta um objeto com as configurações de conexão do mongodb.
    {
      var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString); //Variável para realizar a conexão com o mongodb através da string de conexão.

      var mongoDbDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName); //Referência do banco de dados.
      _pessoasCollection = mongoDbDatabase.GetCollection<Pessoa>("Pessoas"); // Obtem a coleção pessoas e mapea automaticamente para a classe Pessoa.
    }

    public async Task<Pessoa> GetAsync() => //Retorna uma lista de objetos do tipo Pessoa.
    await _pessoasCollection.Find(_ => true).ToListAsync(); //Inicia uma busca e retorna todos os itens da coleção.
    public async Task CreateAsync(Pessoa newPessoa) =>
    await _pessoasCollection.InsertOneAsync(newPessoa);
  }

  public class MongoDbSettings
  {
    public string? ConnectionString { get; set; }
    public string? DataBaseName { get; set; }
  }
}