namespace Ezcel.Prompt.OutputFormatter
{
    public class OutputFormatter
    {
        public enum OutputType
        {
            SingleValue,
            Row,
            Column,
            Range
        }

        public string FormatInstruction(string instruction, OutputType outputType)
        {
            string formatInstruction = string.Empty;

            switch (outputType)
            {
                case OutputType.SingleValue:
                    formatInstruction = "请返回单个值，不要包含任何额外的文本或格式。";
                    break;
                case OutputType.Row:
                    formatInstruction = "请将结果以逗号分隔的形式返回，每个值代表一行中的一个单元格。例如：值1,值2,值3";
                    break;
                case OutputType.Column:
                    formatInstruction = "请将结果以换行符分隔的形式返回，每个值代表一列中的一个单元格。例如：值1\n值2\n值3";
                    break;
                case OutputType.Range:
                    formatInstruction = "请将结果以行换行、列逗号的形式返回，例如：值1,值2\n值3,值4";
                    break;
            }

            return $"{instruction}\n\n{formatInstruction}";
        }
    }
}