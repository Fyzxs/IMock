using System.Threading.Tasks;

namespace DebugSolution.Sample
{
    public interface IFoozzing<T>
    {
        //BEG MockMethod
        void VoidVoid();
        Task ResponseTaskVoid();
        //END MockMethod
        //BEG MockMethodWithParam
        void ParamType(double justOne);
        void ParamGeneric(T justOne);
        Task ParamTypeResponseTask(double justOne);
        void ParamTuple(string justOne, int singleInt);
        void ParamTypeGeneric(string justOne, T oneT);
        //END MockMethodWithParam
        //BEG MockMethodWithResponse
        T ResponseGeneric();
        string ResponseType();
        Task<T> ResponseTaskGeneric();
        Task<double> ResponseTaskType();
        //END MockMethodWithResponse
        //BEG MockMethodWithParamAndResponse
        Task<string> ParamTypeResponseTaskType(int incoming);
        int ParamTupleResponseType(char c, string yeppers, IFooz longer);
        //END MockMethodWithParamAndResponse
        //BEG EdgeCases
        string SameNameDifParams(int one);
        string SameNameDifParams(double one);
        //END EdgeCases
    }
}