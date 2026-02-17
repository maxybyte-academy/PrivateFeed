# How to Import This Presentation into Confluence

## Quick Guide

This document (`CONFLUENCE_PRESENTATION.md`) is formatted with Confluence macros and can be imported directly into Confluence.

### Method 1: Copy & Paste (Recommended)

1. Open the `CONFLUENCE_PRESENTATION.md` file
2. **Copy the entire content** (Ctrl+A, Ctrl+C)
3. In Confluence, create a **new page**
4. Click the **"..."** menu → **Insert** → **Markup**
5. Select **"Confluence Wiki"** format
6. **Paste the content**
7. Click **Insert**
8. Review and publish

### Method 2: Edit in Source Mode

1. Create a new Confluence page
2. Click **Edit**
3. Switch to **Source Editor** (</> icon or Ctrl+Shift+D)
4. Paste the content from `CONFLUENCE_PRESENTATION.md`
5. Switch back to **Visual Editor**
6. Review formatting
7. Publish

## Confluence Macros Used

The document uses standard Confluence macros:

| Macro | Purpose | Renders As |
|-------|---------|------------|
| `{panel}` | Colored boxes with titles | Styled panels |
| `{info}` | Information callouts | Blue info box |
| `{warning}` | Warning messages | Yellow warning box |
| `{tip}` | Tips and recommendations | Green tip box |
| `{note}` | General notes | Gray note box |
| `{code}` | Code blocks with syntax highlighting | Formatted code |

## Customization Tips

### Change Colors

You can customize panel colors in the macro parameters:

```
{panel:titleBGColor=#YOUR_COLOR}
```

Common colors:
- Blue: `#DEEBFF`
- Green: `#E3FCEF`
- Yellow: `#FFFAE6`
- Red: `#FFEBE6`
- Gray: `#F4F5F7`

### Add Images/Screenshots

Where you see placeholders like "Add screenshot here", you can:

1. Click at that location
2. Type `/image` or click Insert → Files and Images
3. Upload or select your screenshot
4. Adjust size as needed

### Expand/Collapse Sections

Some code blocks have `collapse=true`. You can change this to:
- `collapse=false` - Always expanded
- Remove the parameter - Default (collapsed)

## Best Practices

### Before Presenting

- [ ] Add actual screenshots where indicated
- [ ] Update date and presenter information
- [ ] Review all sections for accuracy
- [ ] Test all code examples
- [ ] Verify links work (if any added)
- [ ] Preview on mobile/tablet if needed

### During Presentation

- ✅ Use **Presentation Mode** in Confluence (if available)
- ✅ Expand collapsed code sections as you discuss them
- ✅ Use **Table of Contents** macro for navigation (Confluence can auto-generate)
- ✅ Enable **Comments** for Q&A during/after presentation

### After Presentation

- 📝 Add a Q&A section with questions from the team
- 📝 Update with any feedback or corrections
- 📝 Link to related pages (team wiki, other docs)
- 📝 Add "Last Updated" date

## Confluence Page Settings

### Recommended Settings

**Page Restrictions:**
- Set appropriate view/edit permissions for your team

**Labels/Tags:**
- Add: `poc`, `nuget`, `azure-artifacts`, `dotnet`, `documentation`

**Parent Page:**
- Link to your team's main documentation page or DevOps section

**Watchers:**
- Add relevant team members to get notified of updates

## Troubleshooting

### Macros Not Rendering

**Problem:** Macros appear as plain text
**Solution:**
- Ensure you're using Confluence Wiki markup format
- Try the "Insert Markup" method instead of direct paste

### Formatting Looks Wrong

**Problem:** Tables or panels appear broken
**Solution:**
- Check for special characters that need escaping
- Re-paste using the Insert Markup method
- Switch between Visual and Source editor to refresh

### Code Blocks Without Syntax Highlighting

**Problem:** Code appears as plain text
**Solution:**
- Ensure `{code:language=xxx}` has the correct language
- Supported: `bash`, `csharp`, `yaml`, `xml`, `json`
- Some Confluence versions may need plugins for certain languages

## Enhancing the Presentation

### Add Interactive Elements

1. **Table of Contents**
   - Add at the top: `{toc:printable=true|style=disc|maxLevel=3}`

2. **Status Macros**
   - Use: `{status:colour=Green|title=COMPLETE}`

3. **Task Lists**
   - Use: `{tasks}` macro for interactive checklists

4. **Expand Macro**
   - Wrap long sections: `{expand:title=Click to expand}...{expand}`

### Add Diagrams

For the architecture diagram, consider using:
- **Confluence Diagrams** (draw.io integration)
- **Gliffy** diagrams
- Or keep the ASCII art as is (works well for simple flows)

## Sharing the Presentation

### Export Options

You can export the Confluence page as:
- **PDF** - For offline sharing or printing
- **Word** - For further editing
- **Link** - Share the Confluence page URL

### Presentation Mode

1. Open the page
2. Click **"..."** menu
3. Select **"View in Presentation Mode"** (if available)
4. Use arrow keys to navigate sections

## Version Control

Since this document is in Git:
- Keep the `.md` file updated with any Confluence changes
- Add a "Last Updated" section in Confluence
- Reference the Git repository for version history

## Template for Future POCs

This format can be reused for other POCs:
1. Copy `CONFLUENCE_PRESENTATION.md` as a template
2. Update sections with new POC content
3. Keep the same structure and macros
4. Maintain consistent formatting

---

## Quick Checklist for Confluence Import

- [ ] Copy content from `CONFLUENCE_PRESENTATION.md`
- [ ] Create new Confluence page
- [ ] Use "Insert Markup" → "Confluence Wiki"
- [ ] Paste content
- [ ] Review rendering
- [ ] Add screenshots (optional)
- [ ] Update presenter information
- [ ] Set page permissions
- [ ] Add labels/tags
- [ ] Publish
- [ ] Share link with team

---

**Ready to present!** 🎉

The document is designed to be professional, comprehensive, and easy to navigate in Confluence.
