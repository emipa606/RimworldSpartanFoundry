# GitHub Copilot Instructions for Rimworld: Spartan Foundry (Continued)

### Mod Overview and Purpose

**Rimworld: Spartan Foundry (Continued)** is an update and continuation of the original mod created by Oskar Potocki. This mod enhances the vanilla RimWorld experience by introducing a series of nine advanced, end-game power armor sets inspired by the Halo franchise. These armors offer players exciting new objectives in the form of unique quest rewards and rare loot from raids, enhancing the endgame by providing a sense of accomplishment and advanced technology to strive for.

---

### Key Features and Systems

- **Advanced Power Armor Sets**: Introduces nine new sets of armor with unique bonuses and the ability to combine parts for optimal customization.
- **End-Game Content**: Armors are integrated as rare quest rewards and obtainable through rarely occurring raids, requiring players to progress significantly to acquire them.
- **Shields and Overshield Technology**: Incorporates advanced glitterworld overshield tech based on the Vanilla Expanded Framework, offering defensive capabilities that absorb hits.
- **Visual Variations**: Armor comes in various colors, some standard and some rare, offering diversity in appearance.
- **Compatibility and Support**: Updated for compatibility with Combat Extended and Save Our Ship 2, and includes achievement support.

---

### Coding Patterns and Conventions

- **C# Classes and Methods**: Follow C# standards with clear class and method naming, ensuring readability and maintainability. Each class should encapsulate functionality related to a specific aspect of the mod (e.g., tracking items crafted).
- **Consistent Naming Conventions**: Use PascalCase for class names and camelCase for method names and variables. Maintain consistent prefixing for custom classes with the mod name (e.g., `SpartanFoundry_` for patches).
- **Commenting and Documentation**: Apply XML comments for all public classes and methods to ensure auto-generated documentation is accessible for mod developers.

---

### XML Integration

- **XML Structure**: Use RimWorld's XML structure for defining apparel, items, and technology levels. Each armor set should be carefully defined within `<Defs>` files with the appropriate `<ThingDef>` tags.
- **Balance and Compatibility**: Ensure the armor stats and integration are balanced against the base game mechanics and compatible with popular mods like Combat Extended.
- **Patch Operations**: Utilize Harmony patches where necessary to integrate new functionalities into existing game mechanics, without altering the base game code.

---

### Harmony Patching

- **Method Patching**: Implement Harmony patches to extend game functionality by prefixing, postfixing, or otherwise altering existing methods (e.g., `SpartanFoundry_QuestManager_Notify_ThingsProduced_Patch`).
- **Safe Hooking Practices**: Always check for null references and potential conflicts with other mods to maintain game stability.
- **Documentation and Comments**: Clearly document the purpose of each patch and the expected game behavior changes, to ease troubleshooting and collaboration with other developers.

---

### Suggestions for Copilot

- **Code Suggestions**: Encourage Copilot to adhere to the existing coding patterns such as method and class naming conventions.
- **Refactoring Recommendations**: Use Copilot to suggest methods and code blocks that can be optimized or refactored for better performance and readability.
- **XML Schema Assistance**: Utilize Copilot for generating structured XML definitions for new armor sets, ensuring adherence to RimWorld's schema standards.
- **Patch Recommendations**: Aid in identifying potential areas of the codebase where Harmony patches can enhance mod interactions without causing conflicts.

---

By following these guidelines and making effective use of Copilot's capabilities, Rimworld: Spartan Foundry (Continued) can continue to evolve and provide players with a seamless extension to their RimWorld gameplay.
