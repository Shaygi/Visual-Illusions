# Visuelle Illusionen
Ein interaktiver Prototyp zur Demonstration und Implementierung von visuellen Illusionen in Unity.

## Überblick
Dieses Projekt demonstriert verschiedene visuelle Illusionen, darunter perspektivische Täuschungen, räumliche Illusionen, Größenillusionen und psychologische Illusionen. Es wurde entwickelt, um Entwicklern zu helfen, diese Illusionen in ihren eigenen Projekten zu erkunden und zu implementieren.

## Funktionen
- **Perspektivische Täuschungen:**
    - Spiegelreflektion
    - Ames-Raum
    - Dynamisches Skalieren von Objekten basierend auf der Spielerperspektive
- **Räumliche Illusionen:**
    - Nahtlose Teleportation zwischen Räumen
    - Dynamische Raumveränderung durch Trigger
    - Simulierte räumliche Tiefe
- **Größenillusionen:**
    - Ebbinghaus-Illusion
    - Größenwahrnehmung basierend auf Positionierung
    - Dynamische Größenwahrnehmung basierend auf Spielerentfernung
- **Psychologische Illusionen:**
    - Tunnelblick-Effekte
    - Wärme-, Kälte- und Schmerzempfinden zur Simulation psychologischer Reaktionen

## Voraussetzungen
- Unity Version 2022.3.10f1 oder höher.
- TextMesh Pro (bereits im Projekt enthalten)
- Post Processing Stack v2 für visuelle Effekte.

## Projektstruktur
```bash
Assets/
├── Handbook/                       # Handbuch zur Implementierung der Illusionen
├── Scenes/
│   ├── Menu.unity        # Szene zum Wechseln zwischen Szenen
│   ├── Größen_Illusion.unity        # Szene für Größenillusionen
│   ├── Perspektivische_Täuschung.unity  # Szene für perspektivische Illusionen
│   ├── Psychologische_Illusion.unity    # Szene für psychologische Illusionen
│   └── Räumliche_Illusion.unity     # Szene für räumliche Illusionen
├── Scripts/
│   ├── PerspectiveIllusion/         # Skripte für perspektivische Illusionen
│   ├── SpatialIllusion/             # Skripte für räumliche Illusionen
│   ├── SizeIllusion/                # Skripte für Größenillusionen
│   └── PsychologicalIllusion/       # Skripte für psychologische Illusionen
├── Materials/                       # Materialien
└── RenderTextures/                  # Rendertexturen
```

## Verwendung
1. Öffnen Sie das Projekt in Unity.
2. Navigieren Sie zum Ordner `Assets/Scenes` und laden Sie die Menu Szene oder eine der verfügbaren Illusionsszenen (z. B. `Größen_Illusion.unity`).
3. Drücken Sie „Play“, um die interaktiven Illusionen zu erkunden.

## Integration des Handbuchs
Für detaillierte Implementierungsanweisungen und zusätzliche Ressourcen, siehe das beigefügte [Handbuch] im Hauptprojektverzeichnis /VisualIllusions_Handbook.pdf oder im Verzeichnis Assets/Handbook/VisualIllusions_Handbook.md. Das Handbuch bietet eine schrittweise Anleitung, einschließlich Codeschnipseln, Assets und Best Practices zur Erstellung jeder Illusion.

## Lizenz
Dieses Projekt nutzt kostenlose Assets aus dem Unity Asset Store. Bitte prüfen Sie die jeweiligen Lizenzen für die Nutzungsbedingungen.
