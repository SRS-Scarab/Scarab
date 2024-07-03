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
Make sure that you if you want Unity to open VSCode by default, go to Edit > Preference > External Tools > External Script Editor > select Visual Studio Code
For VSCode itself, it also helps to have the "Unity", "Unity Tools", and "Unity Code Snippets" extensions to help with autocompletion

## Getting Started
Clone the repository
```
git clone https://github.com/SRS-Scarab/Scarab
```
You then open the Scarab folder up with the Unity Editor. This is easiest to do in the Unity Hub, where you simply press Open, navigate to the Scarab folder, then open it. This will open the project up in the unity editor. It typically takes a while to open the first time.

Once you have opened the project, navigate to Assets > Scenes > Showcase to see all of our latest scenes to be showcased. You can open one of these scenes by simply double clicking and then pressing the play button to play that scene!

### Contribution Workflow
While in general, branching and creating PRs is best practice for software development, we have found that there is simply not much need to do this based on our experience. As a result, we simply commit + push directly to main. There should in generaly not be too much changed code overlapping and any merge conflicts should be fairly easy to resolve so long as we don't modify the same scene in the Unity Editor.

With this being said, we must make sure that we don't modify the same scene in Unity. This is why we have created folders in the Scenes folder for each person. The general contribution workflow should look like this:
1. You make sure to pull latest main with `git pull`
2. You copy the scene from the Showcase folder that you want to change into your own folder.
3. You make changes in that copied scene.
4. You make sure to pull the latest main with `git pull`
5. You change the scene in the Showcase folder based on your changes in you copied scene and make sure it works.
6. You save your work in Unity and then add and commit your changes with `git add .` and `git commit -m {your commit message}`
7. Push your changes to the repository with `git push` and you are done! You can check the github site to see your latest commit.

### Large Files
We have some fbx files in the project that are very large and require [LFS](https://docs.github.com/en/repositories/working-with-files/managing-large-files/about-large-files-on-github) to store on github. These files cannot be stored without limits unless we pay for it, which is not really worth, so we have decided for now to simply exclude them from the project. You will instead have to download them from the [shared folder](https://drive.google.com/drive/folders/1Q7_In3082NgcwwA1P7zGd9sCWOmkWYkN) and add them to the project manually afterwards. Hopefully we can fix this issue in the future by reducing the size of our fbx files and remove it from the `.gitignore`.

## Libraries
These are some of the major libraries that we use for your reference. You can see them in the unity project by going to Window -> Package Manager. They are already in the repository so you do not need to add them yourself. Feel free to add any new ones as needed though!
### Yarnspinner
We use this for dialogue. Please see the [documentation](https://docs.yarnspinner.dev/) for more details.
### Unity Toon Shader
We use this for making toon shading. Please see the [documentation](https://docs.unity3d.com/Packages/com.unity.toonshader@0.10/manual/index.html) for more details.
### Input Subsystem
We use this for handling input. Please see the [documentation](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.7/manual/index.html) for more details.
