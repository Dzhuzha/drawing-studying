using System.Collections;
using UnityEngine;

public class TraceSymbolPresenter : MonoBehaviour
{
    private readonly Vector2 _spawnPosition = Vector2.zero;
    private SpellChecker _symbolToDraw;

    public SpellChecker PlaceSymbolOnGamefield(SpellChecker symbolPrefab)
    {
        _symbolToDraw = Instantiate(symbolPrefab, _spawnPosition, Quaternion.identity);
        _symbolToDraw.gameObject.SetActive(false);
        return _symbolToDraw;
    }

    public IEnumerator ShowSymbol() 
    {
        _symbolToDraw.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        _symbolToDraw.ActivateFirstGuideLine();
    }
}