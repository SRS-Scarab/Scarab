# Scarab
A project for Student Run Studios '24, hosted by ACM Studio

## Software needed
- Unity 2022.3.17f1

You can either download the editor directly from here:
https://unity.com/releases/editor/qa/lts-releases

Or download the unity hub to manage your versions from here (this is what I use but its up to you what you prefer):
https://unity.com/releases/editor/archive

- Visual Studio Code

You can use any IDE that you want, but I prefer the VSCode integration with Unity personally.
Make sure that you If you want Unity to open VSCode by default, go to Edit > Preference > External Tools > External Script Editor > select Visual Studio Code
For VSCode itself, it also helps to have the "Unity", "Unity Tools", and "Unity Code Snippets" extensions to help with autocompletion

## Getting Started
Clone the repository
```
git clone https://github.com/SRS-Scarab/Scarab
```
You then open the Scarab folder up with the Unity Editor. This is easiest to do in the Unity Hub, where you simply press Open, navigate to the Scarab folder, then open it. This will open the project up in the unity editor. It typically takes a while to open the first time.

Once you have opened the project, navigate to Assets > Scenes > Demo to see all of our latest scenes to be demoed. You can open one of these scenes by simply double clicking and then pressing the play button to play that scene!

### Contribution Workflow
While in general, branching and creating PRs is best practice for software development, we have found that there is simply not much need to do this based on our experience. As a result, we simply commit + push directly to main. There should in generaly not be too much changed code overlapping and any merge conflicts should be fairly easy to resolve so long as we don't modify the same scene in the Unity Editor.

With this being said, we must make sure that we don't modify the same scene in Unity. This is why we have created folders in the Scenes folder for each person. The general contribution workflow should look like this:
1. You make sure to pull latest main with `git pull`
2. You copy the scene from the Demo folder that you want to change into your own folder.
3. You make changes in that copied scene.
4. You make sure to pull the latest main with `git pull`
5. You change the scene in the Demo folder based on your changes in you copied scene and make sure it works.
6. You save your work in Unity and then add and commit your changes with `git add .` and `git commit -m {your commit message}`
7. Push your changes to the repository with `git push` and you are done! You can check the github site to see your latest commit.
