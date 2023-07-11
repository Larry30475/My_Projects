#include "CMax3SatProblem.h"
#include <iostream>
#include <random>

bool RandomBool(std::mt19937& rng)
{
    return std::uniform_int_distribution<>{0, 1}(rng);
}

void PrintSolvedClauses(const std::vector<std::tuple<int, int, int>>& clauses, std::map<int, bool>& solution)
{
    for (const std::tuple<int, int, int>& clause : clauses)
    {
        const int first = std::get<0>(clause);
        const int second = std::get<1>(clause);
        const int third = std::get<2>(clause);

        std::cout << '(' << first << ' ' << second << ' ' << third << ") ";
        std::cout << std::boolalpha << '(' << solution[first] << ' ' << solution[second] << ' ' << solution[third] << ") ";
        std::cout << (solution[first] || solution[second] || solution[third]) << std::endl;
    }
}

int main()
{
    CMax3SatProblem* problem = CMax3SatProblem::Load("max3sat/50/m3s_50_0.txt");

    const std::set<int>& allPossibleVars = problem->GetPossibleVariables();

    std::mt19937 rng(std::random_device{}());

    std::map<int, bool> solution;

    // Generate random solution
    for (const int& possibleVar : allPossibleVars) 
    {
        solution.insert({possibleVar, RandomBool(rng)});
    }

    std::cout << problem->Compute(solution) << std::endl;

    PrintSolvedClauses(problem->GetClasues(), solution);

    delete problem;
}
