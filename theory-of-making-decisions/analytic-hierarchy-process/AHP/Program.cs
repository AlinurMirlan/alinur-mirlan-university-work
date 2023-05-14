using AHP;
using AHP.AutoMapper;
using AutoMapper;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Net.Http.Headers;

var autoMapperConfig = new MapperConfiguration(config =>
{
    config.AddProfile<AutoMapperProfile>();
});
var mapper = new Mapper(autoMapperConfig);

Console.WriteLine("How many alternatives do you have (from 2 up to 3)?\n" +
    "List them, each one separated with a comma(,):");
string[]? alternatives = Console.ReadLine()?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
if (alternatives is null || alternatives.Length <= 1 || alternatives.Length > 3)
{
    Console.WriteLine("You have to enter at least 2 alternatives or up to 3.");
    return;
}

Console.WriteLine("How many top-level criteria do you want? (from 1 up to 3)?\n" +
    "List them, each one separated with a comma(,):");
string[]? topLevelCriteria = Console.ReadLine()?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
if (topLevelCriteria is null || topLevelCriteria.Length == 0 || topLevelCriteria.Length > 3)
{
    Console.WriteLine("You have to enter at least 1 criterion or up to 3.");
    return;
}

LinkedList<Node> nodes = mapper.Map<LinkedList<Node>>(topLevelCriteria);
BuildHierarchy(null, nodes, 1, 4, mapper);
DisplayHierarchy(nodes, 0);

Console.WriteLine("Fill in your judgments:");
FillJudgments(nodes, mapper);
/*Repeat(() =>
{
    string nodesGlossary = nodes.Aggregate("\t", (glossary, node) => $"{glossary}{node.Name}\t");
    Console.WriteLine(nodesGlossary);
    double[][] judgments = new double[nodes.Count][];
    var node = nodes.First;
    for (int i = 0; i < nodes.Count; i++)
    {
        var stratumNode = node?.Value ?? throw new InvalidOperationException();
        Repeat(() =>
        {
            Console.Write($"{stratumNode.Name}\t");
            string[]? fractions = Console.ReadLine()?.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (fractions is null || fractions.Length < nodes.Count)
            {
                Console.WriteLine("The number of entries ought to match that of the criteria.");
                return false;
            }

            judgments[i] = mapper.Map<double[]>(fractions);
            return true;
        });

        node = node.Next;
    }

    double[,] normalizedJudgments = GetNormalizedJudgments(judgments);
    double[] weights = new double[judgments.Length];
    var weightNode = nodes.First();
    for (int x = 0; x < weights.Length; x++)
    {
        for (int y = 0; y < weights.Length ; y++)
        {
            weights[x] += normalizedJudgments[x, y];
        }

        weights[x] /= weights.Length;
        weightNode.Weight = weights[x];
    }

    if (GetCoherenceRatio(judgments, weights) > 0.1)
    {
        Console.WriteLine("The coherence of your reasoning is flawed. Try reconsidering the judgments.");
        return false;
    }

    return true;
});*/
return;

static void FillJudgments(LinkedList<Node> nodes, IMapper mapper)
{
    Repeat(() =>
    {
        string nodesGlossary = nodes.Aggregate("\t", (glossary, node) => $"{glossary}{node.Name}\t");
        Console.WriteLine(nodesGlossary);
        double[][] judgments = new double[nodes.Count][];
        var listNode = nodes.First;
        for (int i = 0; i < nodes.Count; i++)
        {
            var node = listNode?.Value ?? throw new InvalidOperationException();
            Repeat(() =>
            {
                Console.Write($"{node.Name}\t");
                string[]? fractions = Console.ReadLine()?.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (fractions is null || fractions.Length < nodes.Count)
                {
                    Console.WriteLine("The number of entries ought to match that of the criteria.");
                    return false;
                }

                judgments[i] = mapper.Map<double[]>(fractions);
                return true;
            });

            listNode = listNode.Next;
        }

        double[,] normalizedJudgments = GetNormalizedJudgments(judgments);
        double[] weights = GetAndSetWeights(normalizedJudgments, nodes);
        if (GetCoherenceRatio(judgments, weights) > 0.1)
        {
            Console.WriteLine("The coherence of your reasoning is flawed. Try reconsidering the judgments.");
            return false;
        }

        return true;
    });
}

static double[] GetAndSetWeights(double[,] normalizedJudgments, LinkedList<Node> nodes)
{
    double[] weights = new double[normalizedJudgments.GetLength(0)];
    var listNode = nodes.First;
    for (int x = 0; x < weights.Length; x++)
    {
        for (int y = 0; y < weights.Length; y++)
        {
            weights[x] += normalizedJudgments[x, y];
        }

        weights[x] /= weights.Length;
        listNode!.Value.Weight = weights[x];
        listNode = listNode.Next;
    }

    return weights;
}

static double[,] GetNormalizedJudgments(double[][] judgments)
{
    int judgmentsDimension = judgments.GetLength(0);
    double[,] normalizedJudgments = new double[judgmentsDimension, judgmentsDimension];
    for (int y = 0; y < judgmentsDimension; y++)
    {
        double columnSum = judgments.SumByColumn(y);
        for (int x = 0; x < judgmentsDimension; x++)
        {
            normalizedJudgments[x, y] = judgments[x][y] / columnSum;
        }
    }

    return normalizedJudgments;
}

static double GetCoherenceRatio(double[][] judgments, double[] weights)
{
    Matrix<double> judgmentsMatrix = DenseMatrix.OfRowArrays(judgments);
    Matrix<double> weightsMatrix = DenseMatrix.OfColumnMajor(3, 1, weights);
    double nMax = (judgmentsMatrix * weightsMatrix).ColumnSums().Sum();
    int n = judgmentsMatrix.ColumnCount;
    double CI = (nMax - n) / (n - 1);
    double RI = (1.98 * (n - 2)) / n;
    return CI / RI;
}


static void BuildHierarchy(Node? parentNode, IEnumerable<Node> nodes, int level, int limit, IMapper mapper)
{
    if (level == limit)
    {
        return;
    }

    foreach (var node in nodes)
    {
        node.ParentNode = parentNode;
        Repeat(() =>
        {
            Console.WriteLine($"Does {node.Name} have sub-criteria? (from 2 up to 3)?\n" +
    "List them, each one separated with a comma(,); or press 'Enter' to skip them:");
            string[]? criteria = Console.ReadLine()?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (criteria is null || criteria.Length == 0)
            {
                Console.WriteLine($"{node.Name} is rendered sub-strata-less.");
                return true;
            }
            if (criteria.Length <= 1 || criteria.Length > 3)
            {
                Console.WriteLine("A criterion ought to have at least 2 sub-criteria and no more than 3.");
                return false;
            }

            node.SubNodes = mapper.Map<LinkedList<Node>>(criteria);
            BuildHierarchy(node, node.SubNodes, level + 1, limit, mapper);
            return true;
        });
    }
}

static void DisplayHierarchy(IEnumerable<Node> nodes, int level)
{
    string tab = Enumerable.Range(1, level).Aggregate(string.Empty, (space, _) => space + '\t');
    foreach (var node in nodes)
    {
        Console.WriteLine($"{tab}{node.Name}");
        DisplayHierarchy(node.SubNodes, level + 1);
    }
}

static void Repeat(Func<bool> operation)
{
    bool isFinalized = false;
    while (!isFinalized)
    {
        isFinalized = operation();
    }
}