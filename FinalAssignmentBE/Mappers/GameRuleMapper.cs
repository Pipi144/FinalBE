using AutoMapper;
using FinalAssignmentBE.Dto;
using FinalAssignmentBE.Models;

namespace FinalAssignmentBE.Mappers;

public class GameRuleMapper: Profile
{

    public GameRuleMapper()
    {
        CreateMap<GameRule, GameRuleDto>();
        CreateMap<GameRule, BasicGameRuleDto>();
    }
}