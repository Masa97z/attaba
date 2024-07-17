
using MongoDB.Driver;

public class SectionService : ISectionService
{
    private readonly IMongoCollection<Section> _sections;

    public SectionService(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("CompanyDB");
        _sections = database.GetCollection<Section>("Sections");
    }

    public async Task<Section> GetSectionAsync(string id)
    {
        return await _sections.Find(s => s.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateSectionAsync(Section section)
    {
        await _sections.InsertOneAsync(section);
    }

    public async Task UpdateSectionById(string id, Section section)
    {
        await _sections.ReplaceOneAsync(s => s.Id == id, section);
    }

    public async Task DeleteSectionById(string id)
    {
        await _sections.DeleteOneAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Section>> GetAllSectionsAsync()
    {
        return await _sections.Find(s => true).ToListAsync();
    }
}