using App.Seventh;
using ClassLib.Seventh;
using ClassLib.Seventh.Enums;
using ClassLib.Seventh.LowerBody;
using ClassLib.Seventh.UpperBody;

namespace TestProject.Seventh;

public class RepositoryTestSetup
{
    public Size StandardSize = new Size(180, BodyType.Mesomorph);
    public Fabric BlackWool = new Fabric(FabricType.Natural, System.Drawing.Color.Black);
    public Fabric RedSynthetic = new Fabric(FabricType.Synthetic, System.Drawing.Color.Red);

    public Trousers MalePants1 => new Trousers(StandardSize, Gender.Male, BlackWool, "BrandA");
    public Trousers FemalePantsRed => new Trousers(new Size(165, BodyType.Ectomorph), Gender.Female, RedSynthetic, "BrandB");
    public Jacket MaleJacket => new Jacket(StandardSize, Gender.Male, BlackWool, 3);
    public Skirt FemaleSkirt => new Skirt(new Size(165, BodyType.Ectomorph), Gender.Female, BlackWool, 1.0f);
    public Suit MaleSuit => new Suit(MaleJacket, MalePants1);
}

public class Tests
{
    private AtelierRepository repository;
    private RepositoryTestSetup setup;

    [SetUp]
    public void Setup()
    {
        setup = new RepositoryTestSetup();
        repository = new AtelierRepository();
    }

    [Test]
    public void AddSingleWearableItemIncreasesStorageCount()
    {
        repository.Add(setup.MalePants1);
        Assert.That(repository.GetAll().Count(), Is.EqualTo(1));
    }

    [Test]
    public void AddSuitItemIncreasesStorageCount()
    {
        repository.Add(setup.MaleSuit);
        Assert.That(repository.GetAll().Count(), Is.EqualTo(1));
    }


    [Test]
    public void GetAllReturnsAllAddedItems()
    {
        repository.Add(setup.MalePants1);
        repository.Add(setup.MaleJacket);
        repository.Add(setup.MaleSuit);

        var allItems = repository.GetAll().ToList();

        Assert.That(allItems.Count, Is.EqualTo(3));
        Assert.That(allItems.OfType<Trousers>().Count(), Is.EqualTo(1));
        Assert.That(allItems.OfType<Suit>().Count(), Is.EqualTo(1));
    }


    [Test]
    public void FindTrousersByTypeReturnsCorrectCount()
    {
        repository.Add(setup.MalePants1);
        repository.Add(setup.MaleJacket);
        repository.Add(setup.FemalePantsRed);
        repository.Add(setup.FemaleSkirt);
        
        var trousers = repository.Find<Trousers>(t => true).ToList();

        Assert.That(trousers.Count, Is.EqualTo(2));
        Assert.That(trousers.All(t => t is Trousers), Is.True);
    }

    [Test]
    public void FindSpecificConditionReturnsFilteredItems()
    {
        repository.Add(setup.MalePants1);
        repository.Add(setup.FemalePantsRed); 
        repository.Add(setup.FemaleSkirt);

        var redTrousers = repository
            .Find<Trousers>(t => t.Fabric.Color.ToArgb() == System.Drawing.Color.Red.ToArgb())
            .ToList();

        Assert.That(redTrousers.Count, Is.EqualTo(1));
        Assert.That(redTrousers.First().Fabric.Color, Is.EqualTo(System.Drawing.Color.Red));
        Assert.That(redTrousers.First().Gender, Is.EqualTo(Gender.Female));
    }

    [Test]
    public void FindSuitsByGenderReturnsOnlyMaleSuits()
    {
        var femaleJacket = new Jacket(setup.StandardSize, Gender.Female, setup.BlackWool, 1);
        var femalePants = new Trousers(setup.StandardSize, Gender.Female, setup.BlackWool, "Suit P");
        var femaleSuit = new Suit(femaleJacket, femalePants);
        
        repository.Add(setup.MaleSuit);
        repository.Add(femaleSuit);

        var maleSuits = repository.Find<Suit>(s => s.UpperBody.Gender == Gender.Male).ToList();

        Assert.That(maleSuits.Count, Is.EqualTo(1));
        Assert.That(maleSuits.First().UpperBody.Gender, Is.EqualTo(Gender.Male));
    }

    [Test]
    public void RemoveExistingItemReducesStorageCountAndReturnsTrue()
    {
        var itemToRemove = setup.MalePants1;
        repository.Add(itemToRemove);
        repository.Add(setup.MaleJacket);

        repository.Remove(itemToRemove);
        
        Assert.That(repository.GetAll().Count(), Is.EqualTo(1));
        Assert.That(repository.GetAll().Contains(itemToRemove), Is.False);
    }
    
    [Test]
    public void RemoveExistingSuitReducesStorageCount()
    {
        repository.Add(setup.MalePants1);
        repository.Add(setup.MaleSuit);
        
        repository.Remove(setup.MaleSuit);
        
        Assert.That(repository.GetAll().Count(), Is.EqualTo(1));
        Assert.That(repository.GetAll().OfType<Suit>().Any(), Is.False);
    }
    
    [Test]
    public void RemoveNonExistingItemDoesNotChangeCount()
    {
        repository.Add(setup.MaleJacket);
        var nonExistingItem = setup.FemalePantsRed;
        var initialCount = repository.GetAll().Count();
        repository.Remove(nonExistingItem);
        Assert.That(repository.GetAll().Count(), Is.EqualTo(initialCount));
    }
}
