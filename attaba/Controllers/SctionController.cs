using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class SectionController : ControllerBase
{
    private readonly ISectionService _sectionService;

    public SectionController(ISectionService sectionService)
    {
        _sectionService = sectionService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Section>> GetSectionById(string id)
    {
        var section = await _sectionService.GetSectionAsync(id);
        if (section == null)
        {
            return NotFound();
        }
        return Ok(section);
    }

    [HttpPost]
    public async Task<ActionResult<Section>> CreateSection(Section section)
    {
        if (section == null)
        {
            return BadRequest("Section is null");
        }

        await _sectionService.CreateSectionAsync(section);
        return CreatedAtAction(nameof(GetSectionById), new { id = section.Id }, section);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateSection(string id, Section section)
    {
        if (section == null || id != section.Id)
        {
            return BadRequest("Section ID mismatch");
        }

        var existingSection = await _sectionService.GetSectionAsync(id);
        if (existingSection == null)
        {
            return NotFound();
        }

        await _sectionService.UpdateSectionById(id, section);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSection(string id)
    {
        var section = await _sectionService.GetSectionAsync(id);
        if (section == null)
        {
            return NotFound();
        }

        await _sectionService.DeleteSectionById(id);
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Section>>> GetAllSections()
    {
        var sections = await _sectionService.GetAllSectionsAsync();
        return Ok(sections);
    }
}
