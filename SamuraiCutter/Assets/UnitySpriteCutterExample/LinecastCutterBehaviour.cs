using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnitySpriteCutter;

public class LinecastCutterBehaviour : MonoBehaviour {

	[SerializeField]
	LayerMask layerMask;

    Animator anim;

    LineRenderer line;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        anim = GetComponent<Animator>();
    }

    public void TryLineCastCut(Vector2 slashDir, float slashRange, float slashSpeed)
	{
        ResetLineRenderer();
        anim.SetBool("Slash", true);
        Vector2 originPosition = new Vector3(transform.position.x, transform.position.y, -10f);
        slashDir *= slashRange;
        Vector2 endPosition = originPosition+slashDir;
        StartCoroutine(SlashFX(originPosition, endPosition, slashSpeed));
        LinecastCut(originPosition, endPosition);
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
        if(gameObjectsToCut.Count>0)
        {
            Camera.main.GetComponent<CameraManager>().setShake(0.2f);
            Camera.main.GetComponent<FreezeFX>().FreezeScreen();
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

        if (cuttedObject.GetComponent<HitFX>() != null)
        {
            cuttedObject.GetComponent<HitFX>().Hit();
        }
        if (cuttedObject.GetComponent<Rigidbody2D>()!=null)
        {
            cuttedObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
            cuttedObject.GetComponent<Rigidbody2D>().mass /= 2f;
            cuttedObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        }
        if(cuttedObject.GetComponent<DestructibleObject>()!=null)
        {
            cuttedObject.GetComponent<DestructibleObject>().DestroyObject();
        }
    }

    private void ResetLineRenderer()
    {
        line.enabled = false;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position);
    }

    private IEnumerator SlashFX(Vector2 originPosition, Vector2 endPosition, float slashSpeed)
    {
        line.enabled = true;
        originPosition = new Vector3(originPosition.x, originPosition.y, -10f);
        line.SetPosition(0, originPosition);
        line.SetPosition(1, originPosition);

        Vector2 dirVector = (endPosition - originPosition);
        float amplitude = dirVector.magnitude;
        float curAmplitude = 0f;
        dirVector.Normalize();
       //line.SetPosition(1, endPosition);
        
        float modif = 0f;
        while(curAmplitude<amplitude)
        {
            modif += Time.fixedDeltaTime*slashSpeed;
            line.SetPosition(1, originPosition+(dirVector * modif));
            curAmplitude = ((Vector2)line.GetPosition(1) - originPosition).magnitude;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        modif = 0f;
        curAmplitude = 0f;
        while(curAmplitude<amplitude)
        {
            modif += Time.fixedDeltaTime * (slashSpeed/2f);
            line.SetPosition(0, originPosition + (dirVector * modif));
            curAmplitude = ((Vector2)line.GetPosition(0) - originPosition).magnitude;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        line.enabled = false;
        anim.SetBool("Slash", false);
    }

	bool HitCounts( RaycastHit2D hit )
    {
		return ( hit.transform.GetComponent<SpriteRenderer>() != null ||
		         hit.transform.GetComponent<MeshRenderer>() != null );
	}

}
