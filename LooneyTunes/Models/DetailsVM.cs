namespace LooneyTunes.Models;

public class DetailsVM
{
    public Personagens Anterior { get; set; }

    public Personagens Atual { get; set; }

    public Personagens Proximo { get; set; }
    
    public List<Animais> Animais { get; set; }
}