namespace Logger.Domain
{
    public class ExecutionStepLoggerData : LogBaseData
    {
        public int Step { get; set; }
        /// <summary>
        /// Gets or sets the duration in ms
        /// </summary>
        /// <value>
        /// The duration in ms 
        /// </value>
        public long StepDuration { get; set; }
        public long TotalExecuationDuration { get; set; }
        public ExecutionStepLoggerData()
        {
            Level = LogLevels.Timespan;
        }
    }
}