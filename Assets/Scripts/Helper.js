#pragma strict

public static class Helper extends MonoBehaviour
{
	public function ClampAngle(angle: float, min: float, max: float)
	{
		while (angle < -360 || angle > 360)
		{
			if (angle < -360)
			{
				angle += 360;
			}
			if (angle > 360)
			{
				angle -= 360;
			}
		}
		return Mathf.Clamp(angle, min, max);
	}
}