using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using X_UniTMX;

namespace X_UniTMX
{
	public enum TileAnimationMode
	{
		FORWARD,
		BACKWARD,
		PING_PONG,
		LOOP,
		REVERSE_LOOP
	}
}

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedTile : MonoBehaviour {
	public Map TiledMap;
	public int[] TileFramesGIDs;
	public TileAnimationMode AnimationMode;
	public float AnimationFPS;
	public bool CanAnimate = true;

	List<Sprite> _spriteFrames;
	int _currentFrame = 0;
	float _timer = 0, _animationFPS;

	SpriteRenderer _thisRenderer;

	int _pingPongDirection = 1;

	// Use this for initialization
	void Start () {
		if (TiledMap == null)
		{
			Debug.LogError("AnimatedTile: No TiledMap set!");
			return;
		}

		_thisRenderer = renderer as SpriteRenderer;
		_animationFPS = 1 / AnimationFPS;
		// build sprite frames list
		_spriteFrames = new List<Sprite>();
		Tile tile;
		for (int i = 0; i < TileFramesGIDs.Length; i++)
		{
			if (TiledMap.Tiles.TryGetValue(TileFramesGIDs[i], out tile))
			{
				_spriteFrames.Add(tile.TileSprite);
			}
			else
				Debug.LogWarning("There's no Tile with GID " + TileFramesGIDs[i]);
		}

		if (AnimationMode == TileAnimationMode.BACKWARD || AnimationMode == TileAnimationMode.REVERSE_LOOP)
		{
			_currentFrame = _spriteFrames.Count - 1;
			_thisRenderer.sprite = _spriteFrames[_currentFrame];
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (CanAnimate)
		{
			_timer += Time.deltaTime;
			if (_timer >= _animationFPS)
			{
				_timer = 0;
				switch (AnimationMode)
				{
					case TileAnimationMode.FORWARD:
						_currentFrame++;
						if (_currentFrame >= _spriteFrames.Count - 1)
							CanAnimate = false;
						break;
					case TileAnimationMode.BACKWARD:
						_currentFrame--;
						if (_currentFrame < 1)
							CanAnimate = false;
						break;
					case TileAnimationMode.LOOP:
						_currentFrame++;
						if (_currentFrame > _spriteFrames.Count - 1)
							_currentFrame = 0;
						break;
					case TileAnimationMode.REVERSE_LOOP:
						_currentFrame--;
						if (_currentFrame < 0)
							_currentFrame = _spriteFrames.Count - 1;
						break;
					case TileAnimationMode.PING_PONG:
						_currentFrame += _pingPongDirection;
						if (_currentFrame >= _spriteFrames.Count - 1 || _currentFrame < 1)
							_pingPongDirection = -_pingPongDirection;
						break;
				}
				_thisRenderer.sprite = _spriteFrames[_currentFrame];
			}
		}
	}

	public void Reset()
	{
		CanAnimate = true;
		_timer = 0;
		_pingPongDirection = 1;
		if (AnimationMode == TileAnimationMode.BACKWARD || AnimationMode == TileAnimationMode.REVERSE_LOOP)
			_currentFrame = _spriteFrames.Count - 1;
		else
			_currentFrame = 0;

		_thisRenderer.sprite = _spriteFrames[_currentFrame];
	}
}
