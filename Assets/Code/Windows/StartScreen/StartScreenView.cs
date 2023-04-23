using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Windows.StartScreen
{
    public sealed class StartScreenView : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public IUniTaskAsyncEnumerable<AsyncUnit> ButtonClicks => _button.OnClickAsAsyncEnumerable();
    }
}