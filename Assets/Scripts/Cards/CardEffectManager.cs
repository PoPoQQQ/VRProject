﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectManager : MonoBehaviour
{
	public GameObject yuyukoTachie;
	public TextMesh[] textMeshes;
	public GameObject cardName;
	public SpriteRenderer cardNameFrame;
    public GameObject words;

    public void StartCard(string cardName, bool last = false)
    {
        GetComponent<CardEffectManager2>().StartCard(cardName, last);
        return;
        
    	float duration = 2.7f;
        GameObject.Find("Player").GetComponentInChildren<AudioManager>().PlayCardSE();
        if(!last)
        {
            StartCoroutine(YuyukoTachieMove(duration));
            StartCoroutine(YuyukoTachieFade(duration));
        }
        else
            words.SetActive(false);
    	foreach(TextMesh textMesh in textMeshes)
    		textMesh.text = cardName;
    	StartCoroutine(CardNameAnimation());
    }

    public void EndCard()
    {
        GetComponent<CardEffectManager2>().EndCard();
        return;

        SetCardNameAlpha(0f);
    }

    IEnumerator YuyukoTachieMove(float duration)
    {
    	float amplitude = 7.5f;
    	for(float t = 0; t < duration; t += Time.deltaTime)
    	{
    		SetTachieY(amplitude * (1 - 2 * t / duration));
    		yield return 0;
    	}
    }

    IEnumerator YuyukoTachieFade(float duration)
    {
    	float amplitude = 0.8f;
    	for(float t = 0; t < duration; t += Time.deltaTime)
    	{
    		float x = t / duration;
    		SetTachieAlpha(-4f * (x * x - x) * amplitude);
    		yield return 0;
    	}
        SetTachieAlpha(0f);
    }

    IEnumerator CardNameAnimation()
    {
    	float duration1 = 0.2f;
    	SetCardNameY(-3f);
    	for(float t = 0; t < duration1; t += Time.deltaTime)
    	{
    		SetCardNameZ(-10f + 10f * t / duration1);
    		SetCardNameAlpha(t / duration1);
    		yield return 0;
    	}
    	SetCardNameZ(0f);
    	SetCardNameAlpha(1f);

    	yield return new WaitForSeconds(1f);

    	float duration2 = 1.5f;
    	for(float t = 0; t < duration2; t += Time.deltaTime)
    	{
    		SetCardNameY(-3f + 23f * Mathf.Pow(t / duration2, 2f));
    		SetCardNameAlpha(1 - t / duration2);
    		yield return 0;
    	}
    	SetCardNameY(20f);
    	SetCardNameAlpha(0f);
    }

    void SetTachieY(float y)
    {
    	Vector3 position = yuyukoTachie.transform.localPosition;
    	position.y = y;
    	yuyukoTachie.transform.localPosition = position;
    }

    void SetTachieAlpha(float alpha)
    {
    	Color color = yuyukoTachie.GetComponent<SpriteRenderer>().color;
    	color.a = alpha;
    	yuyukoTachie.GetComponent<SpriteRenderer>().color = color;
    }

    void SetCardNameY(float y)
    {
    	Vector3 position = cardName.transform.localPosition;
    	position.y = y;
    	cardName.transform.localPosition = position;
    }

    void SetCardNameZ(float z)
    {
    	Vector3 position = cardName.transform.localPosition;
    	position.z = z;
    	cardName.transform.localPosition = position;
    }

    void SetCardNameAlpha(float alpha)
    {
    	foreach(TextMesh textMesh in textMeshes)
    	{
    		Color color = textMesh.color;
	    	color.a = alpha;
	    	textMesh.color = color;
    	}
    	{
    		Color color = cardNameFrame.color;
	    	color.a = alpha;
	    	cardNameFrame.color = color;
    	}
    }

    public void WordsAppear()
        => StartCoroutine(WordsAppearCoroutine());

    IEnumerator WordsAppearCoroutine()
    {
        float duration = 2f;
        float initialX = 10f;

        for(float t = 0; t < duration; t += Time.deltaTime)
        {
            Vector3 position = words.transform.position;
            position.x = initialX * (1 - t / duration);
            words.transform.position = position;
            Color color = words.GetComponent<SpriteRenderer>().color;
            color.a = t / duration;
            words.GetComponent<SpriteRenderer>().color = color;
            yield return 0;
        }
        {
            Vector3 position = words.transform.position;
            position.x = 0;
            words.transform.position = position;
            Color color = words.GetComponent<SpriteRenderer>().color;
            color.a = 1;
            words.GetComponent<SpriteRenderer>().color = color;
        }
    }
}
