/******************************************************************************
 * Spine Runtimes License Agreement
 * Last updated May 1, 2019. Replaces all prior versions.
 *
 * Copyright (c) 2013-2019, Esoteric Software LLC
 *
 * Integration of the Spine Runtimes into software or otherwise creating
 * derivative works of the Spine Runtimes is permitted under the terms and
 * conditions of Section 2 of the Spine Editor License Agreement:
 * http://esotericsoftware.com/spine-editor-license
 *
 * Otherwise, it is permitted to integrate the Spine Runtimes into software
 * or otherwise create derivative works of the Spine Runtimes (collectively,
 * "Products"), provided that each user of the Products must obtain their own
 * Spine Editor license and redistribution of the Products in any form must
 * include this license and copyright notice.
 *
 * THIS SOFTWARE IS PROVIDED BY ESOTERIC SOFTWARE LLC "AS IS" AND ANY EXPRESS
 * OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
 * OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN
 * NO EVENT SHALL ESOTERIC SOFTWARE LLC BE LIABLE FOR ANY DIRECT, INDIRECT,
 * INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
 * BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES, BUSINESS
 * INTERRUPTION, OR LOSS OF USE, DATA, OR PROFITS) HOWEVER CAUSED AND ON ANY
 * THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
 * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE,
 * EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *****************************************************************************/

using UnityEngine;
using System.Collections;

namespace Spine.Unity.Examples {
	public class SpineboyPole : MonoBehaviour {
		public SkeletonAnimation skeletonAnimation;
		public SkeletonRenderSeparator separator;

		[Space(18)]
		public AnimationReferenceAsset run;
		public AnimationReferenceAsset pole;
		public float startX;
		public float endX;

		const float Speed = 18f;
		const float RunTimeScale = 1.5f;

		IEnumerator Start () {
			var state = skeletonAnimation.state;

			while (true) {
				// Run phase
				SetXPosition(startX);
				separator.enabled = false; // Disable Separator during run.
				state.SetAnimation(0, run, true);
				state.TimeScale = RunTimeScale;

				while (transform.localPosition.x < endX) {
					transform.Translate(Vector3.right * Speed * Time.deltaTime);
					yield return null;
				}

				// Hit phase
				SetXPosition(endX);
				separator.enabled = true; // Enable Separator when hit
				var poleTrack = state.SetAnimation(0, pole, false);
				yield return new WaitForSpineAnimationComplete(poleTrack);
				yield return new WaitForSeconds(1f);
			}
		}

		void SetXPosition (float x) {
			var tp = transform.localPosition;
			tp.x = x;
			transform.localPosition = tp;
		}
	}

}
