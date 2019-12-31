using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnitySpriteCutter;

public class LinecastCutterBehaviour : MonoBehaviour {

	[SerializeField]
	LayerMask layerMask;

    LineRenderer line;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
    }

    /*
	Vector2 mouseStart;

	void Update() {

		if ( Input.GetMouseButtonDown( 0 ) ) {
			mouseStart = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		}

		Vector2 mouseEnd = Camera.main.ScreenToWorldPoint( Input.mousePosition );

		if ( Input.GetMouseButtonUp( 0 ) ) {
			LinecastCut( mouseStart, mouseEnd );
		}
	}
    */

    public void TryLineCastCut(Vector2 slashDir, float slashRange)
	{
		Vector2 originPosition = transform.position;
        slashDir *= slashRange;
        Vector2 endPosition = originPosition+slashDir;
        line.enabled = true;
        line.SetPosition(0, originPosition);
        line.SetPosition(1, endPosition);
        StartCoroutine(EndLineRenderer());
        LinecastCut(originPosition, endPosition);
	}

    IEnumerator EndLineRenderer()
    {
        yield return new WaitForSeconds(0.1f);
        line.enabled = false;
    }
	
	void LinecastCut( Vector2 lineStart, Vector2 lineEnd )
    {
		List<GameObject> gameObjectsToCut = new List<GameObject>();
		RaycastHit2D[] hits = Physics2D.LinecastAll( lineStart, lineEnd, layerMask );
		foreach ( RaycastHit2D hit in hits )
        {
			if ( HitCounts( hit ) )
            {
				gameObjectsToCut.Add( hit.transform.gameObject );
			}
		}
		
		foreach ( GameObject go in gameObjectsToCut )
        {
			SpriteCutterOutput output = SpriteCutter.Cut( new SpriteCutterInput()
            {
				lineStart = lineStart,
				lineEnd = lineEnd,
				gameObject = go,
				gameObjectCreationMode = SpriteCutterInput.GameObjectCreationMode.CUT_OFF_COPY,
			} );
            ModifyOutputObjects(output.firstSideGameObject);
            ModifyOutputObjects(output.secondSideGameObject);
		}
	}

    private void ModifyOutputObjects(GameObject cuttedObject)
    {
        if(cuttedObject.GetComponent<Rigidbody2D>()!=null)
        {
            cuttedObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
            cuttedObject.GetComponent<Rigidbody2D>().mass /= 2f;
            cuttedObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        }
    }

	bool HitCounts( RaycastHit2D hit )
    {
		return ( hit.transform.GetComponent<SpriteRenderer>() != null ||
		         hit.transform.GetComponent<MeshRenderer>() != null );
	}

}
