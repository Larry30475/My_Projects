#pragma once

#include <string>
#include <vector>
#include <tuple>
#include <map>
#include <set>

class CMax3SatProblem
{
private:
    std::vector<std::tuple<int, int, int>> _clauses;
    std::set<int> _possibleVariables;

public:
    CMax3SatProblem(const std::vector<std::tuple<int, int, int>>& clauses, const std::set<int>& possibleVariables);

    static CMax3SatProblem* Load(const char* path);

    int Compute(std::map<int, bool>& solution);
    const std::set<int>& GetPossibleVariables() { return _possibleVariables; }
    const std::vector<std::tuple<int, int, int>>& GetClasues() { return _clauses; }
};
