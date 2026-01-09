import sqlite3
import tkinter as tk
from tkinter import messagebox

# ==============================
# Configuración de la base de datos
# ==============================
def init_db():
    conn = sqlite3.connect("songs.db")
    cursor = conn.cursor()
    cursor.execute("""
        CREATE TABLE IF NOT EXISTS songs (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            songName VARCHAR(255),
            songType VARCHAR(255),
            songURL VARCHAR(255) UNIQUE,
            userName VARCHAR(255),
            userURL VARCHAR(255),
            isRecommended INTEGER DEFAULT 0
        )
    """)
    conn.commit()
    conn.close()

# ==============================
# Función para insertar datos
# ==============================
def save_song():
    name = entry_name.get().strip()
    type_ = entry_type.get().strip()
    url = entry_url.get().strip()
    user = entry_user.get().strip()
    user_url = entry_user_url.get().strip()
    recommended = entry_recommended.get().strip()

    if not name or not type_ or not url or not user or not user_url:
        messagebox.showwarning("Campos vacíos", "Por favor, completa todos los campos obligatorios.")
        return

    try:
        is_recommended = 1 if int(recommended) == 1 else 0
    except ValueError:
        is_recommended = 0

    conn = sqlite3.connect("songs.db")
    cursor = conn.cursor()

    # Comprobar si la URL ya existe
    cursor.execute("SELECT id FROM songs WHERE songURL = ?", (url,))
    if cursor.fetchone():
        conn.close()
        messagebox.showwarning("Duplicado", "Esta canción ya está registrada (la URL ya existe).")
        return

    try:
        cursor.execute("""
            INSERT INTO songs (songName, songType, songURL, userName, userURL, isRecommended)
            VALUES (?, ?, ?, ?, ?, ?)
        """, (name, type_, url, user, user_url, is_recommended))
        conn.commit()
    except sqlite3.IntegrityError:
        messagebox.showwarning("Duplicado", "Esta canción ya está registrada.")
    finally:
        conn.close()

    entry_name.delete(0, tk.END)
    entry_type.delete(0, tk.END)
    entry_url.delete(0, tk.END)
    entry_user.delete(0, tk.END)
    entry_user_url.delete(0, tk.END)
    entry_recommended.delete(0, tk.END)

    messagebox.showinfo("Éxito", "Canción añadida correctamente.")

# ==============================
# Interfaz gráfica
# ==============================
init_db()

root = tk.Tk()
root.title("Gestor de canciones")
root.geometry("400x360")
root.resizable(False, False)

# Etiquetas y campos
tk.Label(root, text="Nombre de la canción:").pack(anchor="w", padx=10, pady=(10, 0))
entry_name = tk.Entry(root, width=50)
entry_name.pack(padx=10)

tk.Label(root, text="Tipo de canción:").pack(anchor="w", padx=10, pady=(10, 0))
entry_type = tk.Entry(root, width=50)
entry_type.pack(padx=10)

tk.Label(root, text="URL de la canción:").pack(anchor="w", padx=10, pady=(10, 0))
entry_url = tk.Entry(root, width=50)
entry_url.pack(padx=10)

tk.Label(root, text="Nombre del usuario:").pack(anchor="w", padx=10, pady=(10, 0))
entry_user = tk.Entry(root, width=50)
entry_user.pack(padx=10)

tk.Label(root, text="URL del usuario:").pack(anchor="w", padx=10, pady=(10, 0))
entry_user_url = tk.Entry(root, width=50)
entry_user_url.pack(padx=10)

tk.Label(root, text="¿Recomendada? (1 = Sí, 0 = No):").pack(anchor="w", padx=10, pady=(10, 0))
entry_recommended = tk.Entry(root, width=10)
entry_recommended.insert(0, "0")
entry_recommended.pack(padx=10)

# Botón de guardar
tk.Button(root, text="Guardar canción", command=save_song, bg="#4CAF50", fg="white").pack(pady=15)

root.mainloop()
