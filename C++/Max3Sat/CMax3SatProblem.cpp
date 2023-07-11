#include "CMax3SatProblem.h"
#include <fstream>

CMax3SatProblem::CMax3SatProblem(const std::vector<std::tuple<int, int, int>>& clauses, const std::set<int>& possibleVariables)
{
    this->_clauses = clauses;
    this->_possibleVariables = possibleVariables;
}

int CMax3SatProblem::Compute(std::map<int, bool>& solution)
{
    int result = 0;

    for (const std::tuple<int, int, int>& clause : this->_clauses) 
    {
        const int first = std::get<0>(clause);
        const bool firstMapped = solution[abs(first)];

        if ((firstMapped == (first > 0))) 
        {
            result++;
            continue;
        }

        const int second = std::get<1>(clause);
        const bool secondMapped = solution[abs(second)];

        if ((secondMapped == (second > 0))) 
        {
            result++;
            continue;
        }

        const int third = std::get<2>(clause);
        const bool thirdMapped = solution[abs(third)];

        if ((thirdMapped == (third > 0))) 
        {
            result++;
        }
    }

    return result;
}

CMax3SatProblem* CMax3SatProblem::Load(const char* path) 
{
    std::ifstream fileStream(path, std::ios_base::in);

    char _; // skip parentheses
    int x1, x2, x3;
    
    std::vector<std::tuple<int, int, int>> clauses;
    std::set<int> possibleVariables;

    while (fileStream >> _)
    {
        fileStream >> x1 >> x2 >> x3;
        fileStream >> _;

        clauses.push_back({x1, x2, x3});
        possibleVariables.insert({abs(x1), abs(x2), abs(x3)});
    }

    fileStream.close();

    return new CMax3SatProblem(clauses, possibleVariables);
}