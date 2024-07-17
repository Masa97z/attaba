public interface ISectionService
{
	Task<Section> GetSectionAsync(string id);
	Task CreateSectionAsync(Section section);
	Task UpdateSectionById(string id, Section section);
	Task DeleteSectionById(string id);
	Task<IEnumerable<Section>> GetAllSectionsAsync();
}