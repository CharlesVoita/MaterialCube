using System.Collections;
using TMPro;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public int currency = 0;

    public int life = 1000;

    protected const int HEALTH_HEIGHT = 30;

    public TextMeshProUGUI currencyText;
    public RectTransform healthBar;
    private AudioSource source;
    public AudioClip deathSound;

    public SpriteRenderer cover;

    public TextMeshProUGUI gameName;
    public TextMeshProUGUI ldText;

    private void Start()
    {
        currencyText.text = "" + currency;
        //healthBar.setWidth;
        healthBar.sizeDelta = new Vector2 (life, HEALTH_HEIGHT);
        source = GetComponent<AudioSource>();
    }

    public void addToCurrency(int add)
    {
        if (add > 0) {
            StartCoroutine(addCurrency(add));
        } else {
            StartCoroutine(subtractCurrency(add));
        }
    }

    IEnumerator addCurrency(int add)
    {
        var targetCurrency = currency + add;
        var iterateCurrency = currency;

        while (iterateCurrency < targetCurrency) {
            iterateCurrency += 10;
            currencyText.text = "" + iterateCurrency;
            yield return new WaitForSeconds(.03f);
        }
        currency = targetCurrency;
        currencyText.text = "" + currency;

    }
    
    IEnumerator subtractCurrency(int add)
    {
        var targetCurrency = currency + add;
        var iterateCurrency = currency;

        while (iterateCurrency > targetCurrency) {
            iterateCurrency -= 10;
            currencyText.text = "" + iterateCurrency;
            yield return new WaitForSeconds(.03f);
        }
        currency = targetCurrency;
        currencyText.text = "" + currency;

    }
    
    public void addToHealth(int add)
    {
        if (add > 0) {
            StartCoroutine(addHealth(add));
        } else {
            StartCoroutine(subtractHealth(add));
        }
    }
    
    IEnumerator addHealth(int add)
    {
        var targetLife = life + add;
        var iterateLife = life;

        while (iterateLife < targetLife) {
            iterateLife += 2;
            healthBar.sizeDelta = new Vector2 (iterateLife, HEALTH_HEIGHT);
            yield return new WaitForSeconds(.03f);
        }
        life = targetLife;
        healthBar.sizeDelta = new Vector2 (life, HEALTH_HEIGHT);

    }
    
    IEnumerator subtractHealth(int add)
    {
        var targetLife = life + add;
        var iterateLife = life;

        while (iterateLife > targetLife) {
            iterateLife -= 2;
            healthBar.sizeDelta = new Vector2 (iterateLife, HEALTH_HEIGHT);
            yield return new WaitForSeconds(.03f);
        }
        life = targetLife;
        healthBar.sizeDelta = new Vector2 (life, HEALTH_HEIGHT);

    }
    
    public IEnumerator flashCost()
    {
        currencyText.color = new Color32(0xD1, 0xF1, 0xFF, 0x00);
        yield return new WaitForSeconds(.1f);
        currencyText.color = new Color32(0xD1, 0xF1, 0xFF, 0xFF);
        yield return new WaitForSeconds(.1f);
        currencyText.color = new Color32(0xD1, 0xF1, 0xFF, 0x00);
        yield return new WaitForSeconds(.1f);
        currencyText.color = new Color32(0xD1, 0xF1, 0xFF, 0xFF);
        yield return new WaitForSeconds(.1f);
        currencyText.color = new Color32(0xD1, 0xF1, 0xFF, 0x00);
        yield return new WaitForSeconds(.1f);
        currencyText.color = new Color32(0xD1, 0xF1, 0xFF, 0xFF);
        yield return new WaitForSeconds(.1f);
        currencyText.color = new Color32(0xD1, 0xF1, 0xFF, 0x00);
        yield return new WaitForSeconds(.1f);
        currencyText.color = new Color32(0xD1, 0xF1, 0xFF, 0xFF);
        yield return new WaitForSeconds(.1f);
    }

    public void hideTitle()
    {
        gameName.enabled = false;
    }
    
    public void hideLdText()
    {
        ldText.enabled = false;
    }

    public void endGame()
    {
        cover.enabled = true;
        gameName.enabled = true;
        currencyText.enabled = false;
        ldText.enabled = true;
    }
    

    public bool checkForTrueDeath()
    {
        if (life <= 0) {
            life = 0;
            Debug.Log("You have served your purpose. You are now dead.");
            source.PlayOneShot(deathSound);
            return true;
        }

        return false;
    }
}
