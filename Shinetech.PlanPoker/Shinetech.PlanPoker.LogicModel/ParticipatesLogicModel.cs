﻿namespace Shinetech.PlanPoker.LogicModel
{
    public class ParticipatesLogicModel
    {
        public int Id { get; set; }
        public bool IsRegister { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int ProjectId { get; set; }
    }
}
